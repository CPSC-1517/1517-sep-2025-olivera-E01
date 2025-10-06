namespace LibraryBooks.ClassLibrary
{
    /// <summary>
    /// Data class for a single book, with state validation & control over data mutation. 
    /// </summary>
    public class Book
    {
        // Design spec (fulfill these requirements):

        //   - should have the following data fields:
        //       - minimum # of pages (expressed as a private constant)
        //       - ISBN (cannot be null/blank)
        //       - book title (cannot be null/blank)
        //       - author name (cannot be null/blank)
        //       - # of pages (must be at least minimum # of pages)

        //   - should have the following behaviour:
        //       - no data fields above should be externally mutable (modifiable)
        //       - a public method ChangeTitle that exposes/allows changing the book's title
        //       - a public method ChangePages that exposes/allows changing the book's page count
        //       - an overriden ToString method that displays ISBN, title, author, # pages
    }
}
