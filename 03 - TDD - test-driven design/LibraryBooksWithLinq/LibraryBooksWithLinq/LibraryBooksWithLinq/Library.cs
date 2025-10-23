using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// same access syntax as before, but with LINQ-enabled list

namespace LibraryBooksWithLinq;

public class Library
{
    private List<Book> _books = new();
    public List<Book> Books => _books;

    public void AddBook(Book book)
    {
        if (book is null)
        {
            throw new ArgumentNullException(nameof(book), "Book cannot be null");
        }

        _books.Add(book);
    }

    public void RemoveBook(string title)
    {
        Book? book = FindByTitle(title);
        if (book is null)
        {
            throw new ArgumentException($"Title {title} does not exist");
        }

        Books.Remove(book);
    }

    private Book? FindByTitle(string title)
    {
        Book? foundBook = null;
        for (int i = 0; i < _books.Count && foundBook is null; i++)
        {
            if (Books[i].Title == title)
            {
                foundBook = _books[i];
            }
        }

        return foundBook;
    }

}



