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
            get => _libraryId;
            private set
            {
                // validate that we don't get a null, blank, or empty string
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("LibraryId cannot be null or whitespace.", nameof(value));
                }
                
                _libraryId = value.Trim();
            }
        }

        public string Name
        {
            get => _name;
            private set
            {
                // validate that we don't get a null, blank, or empty string
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace.", nameof(value));
                }

                _name = value.Trim();                
            }
        }

        public string? Address
        {
            get => _address;
            private set
            {
                // allow nullability, BUT *if* non-null, then do non-empty/non-blank validation
                if (value is null)
                {
                    _address = null;
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Address values cannot be blank.", nameof(value));
                }
            }
        }

        public int TotalBooks => Books.Count;


        // composite constructor
        public Library(string libraryId, string name, string? address = null, List<Book>? books = null)
        {
            // pass parameters to public property, *not* private data field, so that input validation runs
            LibraryId = libraryId;
            Name = name;
            Address = address;
            
            // a little bit of conditional/validation logic for our list of books
            if (books != null)  //   == -> equals,    != -> not equals
            {
                // I want to check that there are no duplicate books in the input list
                
                // write two nested loops, so that we can compare each book against the previous one
                // I'm starting i at index/counter 0 and x at index/counter 1 because e.g. once I've checked the first
                // book against everything after it, I don't need to check the second, third, etc. books against the first, etc.
                for (int i = 0; i < books.Count; i++)
                {
                    for (int x = i + 1; x < books.Count; x++)
                    {
                        // if I have some list my_list = ["a", "b", "c"], I can access each indiv. element by its index
                        //                  at position:   0    1    2          <-- iterable indices starts at 0
                        // I can access an element at a specific indexical position with my_list[index_number]
                        // So e.g. above, my_list[1] -> "b"
                        if (IsSameIsbn(books[i].ISBN, books[x].ISBN))
                        {
                            throw new ArgumentException($"Duplicate ISBN: {books[i].ISBN}.", nameof(books));
                        }
                    }
                }
                
                // outside of that entire process of checking for duplicates, if all good, populate our list!
                Books = new List<Book>(books);
            }
        }

        // a 'book' I'm trying to add a) can't be null, and b) must be ISBN-unique
        public void AddBook(Book book)
        {
            // a) check for nullability
            if (book is null)
            {
                throw new ArgumentNullException(nameof(book), "AddBook: book param canot be null");
            }
            
            // b) check for duplicate ISBN.
            bool isDuplicateIsbn = FindByIsbn(book.ISBN) is not null;  // if this is null, FindByIsbn lookup found no match

            if (isDuplicateIsbn)
            {
                throw new ArgumentException($"Book ISBN {book.ISBN} already exists; duplicates are not allowed.");
            }
            
            // c) otherwise, add to books
            Books.Add(book);

        }

        public void RemoveBook(string isbn)
        {
            // a) validate & trim isbn
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentNullException(nameof(isbn), "ISBN cannot be empty");
            }
            // b) find matching book OR throw exception for missing book
            var foundBook = FindByIsbn(isbn);
            if (foundBook == null)
            {
                throw new ArgumentException($"Cannot remove ISBN {isbn}; book not found in library.");
            }
            // c) if there's a match, just remove the book
            Books.Remove(foundBook);
        }

        public Book? FindByIsbn(string isbn)
        {
            Book? getSingleResult = null;
            for (int i = 0; i < Books.Count && getSingleResult is null; i++)
            {
                if (IsSameIsbn(Books[i].ISBN, isbn))
                {
                    getSingleResult = Books[i];
                }
            }
            return getSingleResult;
        }

        private static bool IsSameIsbn(string a, string b) =>
            // I want to ignore case, otherwise e.g. A1b1 will be interpreted as distinct from a1b1
            string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
    }
}