using System.Runtime.CompilerServices;

namespace LibraryBooks.ClassLibrary
{
    /// <summary>
    /// Data class for a single book, with state validation & control over data mutation. 
    /// </summary>
    public class Book
    {
        private const int MIN_PAGES = 10;

        // string is a record type, so its default value will be null
        // --> that is *intended* so i will acommodate it
        // --> in this case, ! is a "null-forgiving" operator, a.k.a. null warning suppressor
        //
        //   Why not just use "" as the default value? I could, *but* defaulting to null makes it
        // clearer and easier to tell/query for what has been instantiated, modified, etc. 

        private string _isbn   = default!;  // a.k.a. "dammit operator" or null-forgiving
        private string _title  = default!;
        private string _author = default!;
        private int    _pages;

        public string ISBN
        {
            get => _isbn;
        }

        public string Title
        {
            get => _title;
        }

        public string Author
        {
            get => _author;
        }

        public int Pages
        {
            get => _pages;
        }

        public Book (string isbn, string title, string author, int pages)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            Pages = pages;
        }

        public void ChangeTitle(string newTitle) => Title = newTitle;

        public void ChangePages(int newPages) => Pages = newPages; 

        public override string ToString() => $"{ISBN} - {Title} - {Author}, {Pages}pp.";


        //   - should have the following behaviour:
        //       - no data fields above should be externally mutable (modifiable)
    }
}
