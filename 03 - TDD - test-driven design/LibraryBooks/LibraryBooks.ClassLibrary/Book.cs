using System.Runtime.CompilerServices;

namespace LibraryBooks.ClassLibrary
{
    /// <summary>
    /// Data class for a single book, with state validation & control over data mutation. 
    /// </summary>
    public class Book
    {
        private const int MIN_PAGES = 10;

        private string _isbn   = 
        private string _title  =
        private string _author =
        private int    _pages;

        public string ISBN
        {

        }

        public string Title
        {

        }

        public string Author
        {

        }

        public int Pages
        {

        }

        public Book (string isbn, string title, string author, int pages)
        {

        }

        public void ChangeTitle() { }

        public void ChangePages() { }

        public override string ToString()
        {

        }


        //   - should have the following behaviour:
        //       - no data fields above should be externally mutable (modifiable)
    }
}
