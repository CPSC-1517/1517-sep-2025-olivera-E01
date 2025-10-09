// unfortunately I am *absolutely* trolling you by demoing an example based on
// a class called Library in a class library,
// but dw; we're all gonna make it

namespace LibraryBooks.ClassLibrary
{
    /// <summary>
    /// A container of multiple Book items.
    /// </summary>
    public class Library
    {
        // private fields
        private string _libraryId = default!;
        private string _name = default!;
        private string? _address;  // ? means nullable. "maybe a string, maybe null"

        // collection of instances from another class
        public List<Book> Books { get; private set; } = new();

        // properties
        public string LibraryId
        {

        }

        public string Name
        {

        }

        public string? Address
        {

        }

        public int TotalBooks => Books.Count;


        // constructor
        public Library()
        {

        }

        // methods i want:
        public void AddBook(Book book)
        {
        
        }

        public void RemoveBook(string isbn)
        {

        }

        public Book? FindByIsbn(string isbn)
        {

        }
    }
}