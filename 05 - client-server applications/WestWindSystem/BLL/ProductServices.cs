
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

        #endregion
    }
}
