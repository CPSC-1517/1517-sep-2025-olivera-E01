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
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(ISBN), "ISBN is required (non-empty, non-blank).");
                }

                _isbn = value.Trim();
            }

        }

        public string Title
        {
            get => _title;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(Title), "Title is required (non-empty, non-blank).");
                }

                _title = value.Trim();
            }
        }

        public string Author
        {
            get => _author;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(Author), "Author is required (non-empty, non-blank).");
                }

                _author = value.Trim();
            }
        }

        public int Pages
        {
            get => _pages;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"Pages must be a positive non-zero number. Received: {value}.", nameof(Pages));
                }

                if (value < MIN_PAGES)
                {
                    throw new ArgumentException($"Pages must be at least {MIN_PAGES}. Received {value}.", nameof(Pages));
                }
            }
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


    }
}
