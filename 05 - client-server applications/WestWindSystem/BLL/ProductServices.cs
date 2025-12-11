
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
#endregion

namespace WestWindSystem.BLL
{
    public class ProductServices
    {
        #region setup of the context connection variable and class constructor
        private readonly WestWindContext _context;

        internal ProductServices(WestWindContext registeredcontext)
        {
            _context = registeredcontext;
        }
        #endregion


        // set up product queries in anticipation of a search page
        #region Queries
        public List<Product> Product_GetByCategoryID(int categoryid)
        {
            IEnumerable<Product> info = _context.Products
                                        .Where(x => x.CategoryID == categoryid)
                                        .OrderBy(x => x.ProductName);
            return info.ToList();
        }

        public Product Product_GetByID(int productid)
        {
            Product info = _context.Products
                                .FirstOrDefault(x => x.ProductID == productid);
            return info;
        }
        #endregion




        #region CRUD (Add, Update and Delete)
        public int Product_Add(Product item)
        {
            // Adding a record to your database may require additional validation that was not
            // done on the front end, such as, was data actually passed to the method?
            //
            // Here, we're not leaving the DB responsible for setting primary key; 'us'/the user is.
            // Therefore, we should also check to see if the PK already exists.
            //
            //  There could be business/logic rules that need to check the instance data against
            //  existing DB records, e.g. if a value or relationship needs to be unique.

            // checking if data was even passed in
            if (item == null)
            {
                throw new ArgumentNullException("Product information was not submitted.");
            }

            // check if PK/row/instance already exists in DB: same supplier, product name, qty per unit?
            bool exists = false;
            exists = _context.Products
                            .Any(x => x.SupplierID == item.SupplierID
                                    && x.ProductName.Equals(item.ProductName)
                                    && x.QuantityPerUnit == item.QuantityPerUnit);
            if (exists)
                throw new ArgumentException($"{item.ProductName} from " +
                                            $"{item.Supplier.CompanyName} of size " +
                                            $"{item.QuantityPerUnit} already on file.");


            // If all validation passes, let's assume we're good to write to the DB.
            // If a field is the PK/identity field, set it to zero to prevent IDENTITY_INSERT DB errors first.
            item.ProductID = 0;

            // Similar to how we use git, we're 'staging' and then 'committing' changes:

            //Staging
            //EntityFramework sets up all DB processing in local memory first
            //what is needed for staging
            // a) know the DbSet : Products
            // b) know the action : Add
            // c) know the instance of the DbSet to use: item

            //IMPORTANT: the data is in LOCAL MEMORY
            //           the data is NOT!!! yet been sent to the database
            //THEREFORE: at this time, there is NO!!!!! IDENTITY primary key value
            //              on this instance (except for the default of the datatype)
            //UNLESS: you have place a value in the NON_IDENTITY key field(s)
            _context.Products.Add(item);

            //Commit
            // this sends the ALL staged data in local memory to the database for processing

            //ANY annotation validation in your entity is executed to validate the data
            //  going to the database
            //if there is a validation problem then an exception is thrown and processing of
            //  the commit is terminated (transaction RollBack)
            _context.SaveChanges(); //if successful, data is committed

            // After a successful DB commit, the new product ID is available.
            // While you don't *need* to return a value in a create method, it's good practice
            // especially because it provides 'proof' that the operation was successful.

            return item.ProductID;
        }


        public int Product_Update(Product item)
        {
            //was data passed in
            if (item == null)
            {
                throw new ArgumentNullException("Product information was not submitted.");
            }

            //does the pkey exist?
            if (!_context.Products.Any(x => x.ProductID == item.ProductID))
            {
                throw new ArgumentException($"{item.ProductName}  of size " +
                                            $"{item.QuantityPerUnit}is not on file. " +
                                            "Check for the product again.");
            }

            bool exists = false;
            exists = _context.Products
                            .Any(x => x.SupplierID == item.SupplierID
                                    && x.ProductName.Equals(item.ProductName)
                                    && x.QuantityPerUnit == item.QuantityPerUnit
                                    && x.ProductID != item.ProductID);
            if (exists)
                throw new ArgumentException($"{item.ProductName} from " +
                                            $"{item.Supplier.CompanyName} of size " +
                                            $"{item.QuantityPerUnit} already on file.");



            // 'stage' changes
            EntityEntry<Product> updating = _context.Entry(item);
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            // 'commit' themn
            return _context.SaveChanges(); //if successful, return row affected count

        }


        //Delete: cruD
        //there are two types of deletes: physical and logical
        //Whether you have a physical or logical delete is determined WHEN
        //  the system is designed (database, data requirements)



        //Logical delete
        //this happens when the records is deemed "unwanted" BUT CANNOT be 
        //  physically removed from the database because the records has
        //  a relationship to another records (parent/child) and the associated record
        //  CANNOT be removed



        //Example: The product record is a parent to ManitfestItems records
        //         The manifest record is need for tracking, it goes to the receiver of the product
        //so, because the other record(s) are required for the business
        //      one CANNOT physically remove the ("parent") product record.



        //usually in this situation, the parent record (product) will have some type of field
        //  that will indicate "deleted"
        //on the product record such a field is the Discontinued field



        //Question: If the record will not be deleted, what happens?
        //Answer: here, you will actually do an update
        //Within the method, it is a good practice NOT to rely on the user to set
        //  the "logical delete" field to the delete status
        //Your method should set the value

        public int Product_LogicalDelete(Product item)
        {
            //was data passed in
            if (item == null)
            {
                throw new ArgumentNullException("Product information was not submitted.");
            }

            //does the pkey exist?
            //here we will want the actual db record so that it can be altered BEFORE staging
            //we do not want to use the incoming record.
            Product exists = null;
            exists = _context.Products
                                .FirstOrDefault(x => x.ProductID == item.ProductID);

            if (exists == null)
            {
                throw new ArgumentException($"{item.ProductName}  of size " +
                                            $"{item.QuantityPerUnit} is not on file. " +
                                            "Check for the product again.");
            }

            // we're using Product.Discontinued to 'take this product out of commission' (logical delete from system behaviour),
            // rather than 'burning the body' (physiical delete from DB)
            // Basically, we've implemented a property that lets us take it out of action, and we'll toggle that & then *update* the instance.
            exists.Discontinued = true;  // (this is a property that already existed on Product, as a design choice, for this type of reason!)

            //Staging
            EntityEntry<Product> updating = _context.Entry(item);
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //Commit
            return _context.SaveChanges(); //if successful, return row affected count
        }

        //Physical Delete
        //you physically remove the record from the database
        //IF there are no "child" records to prevent the record removal, you can remove the record
        //IF there are "children" AND the "children" are not required, you can remove the record
        //      HOWEVER, you will need to first remove any "children" before removing the parent record
        //      assuming there is no cascade delete setup on the database

        public int Product_PhysicalDelete(Product item)
        {
            //was data passed in
            if (item == null)
            {
                throw new ArgumentNullException("Product information was not submitted.");
            }

            //does the pkey exist?
            if (!_context.Products.Any(x => x.ProductID == item.ProductID))
            {
                throw new ArgumentException($"{item.ProductName}  of size " +
                                            $"{item.QuantityPerUnit}is not on file. " +
                                            "Check for the product again.");
            }

            //this delete assumes that there is no appropriate field on the 
            //  record to indicate a logical "delete" and thus: a physical
            //  delete will occur

            //HOWEVER!! this record could be a parent to one or more "child" records
            //One should ensure that there is no existing child record for the
            //  parent BEFORE attempting the delete


            //using the virtual navigational properties, one could check to see
            //  if any child records (collection) exists for the parent
            //if there is a cascade delete setup on your dataset and is allowed
            //  then these checks are unnecessary

            if (_context.Products.Any(x => x.ManifestItems.Count > 0))
            {
                throw new ArgumentException($"{item.ProductName}  of size " +
                                            $"{item.QuantityPerUnit} has Manifest records on file. Unable to delete.");
            }

            if (_context.Products.Any(x => x.OrderDetails.Count > 0))
            {
                throw new ArgumentException($"{item.ProductName}  of size " +
                                            $"{item.QuantityPerUnit} has Order detail records on file. Unable to delete.");
            }

            //Staging
            EntityEntry<Product> deleting = _context.Entry(item);
            deleting.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            //Commit
            return _context.SaveChanges(); //if successful, return row affected count
        }

        #endregion
    }
}
