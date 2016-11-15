using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
  class LibraryModel
  {
    
    private List<Book> LibBooks; //List of books in library
    private List<Visitor> LibVisitors; //List of visitors in library
    
    /* Default constructor */
    public LibraryModel()
    {
      LibBooks = new List<Book>();
      LibVisitors = new List<Visitor>();
    }

    /* Adding book to book's list function */
    public void AddBook(string Name)
    {
      LibBooks.Add(new Book(Name, LibBooks.Count + 1));
    }

    /* Adding visitor to visitor's list function */
    public void AddVisitor(string Name)
    {
      LibVisitors.Add(new Visitor(Name, LibVisitors.Count+1));
    }

    /* Removing book from book's list function by id */
    public void DeleteBook(int Id)
    {
      int BookIndex = LibBooks.FindIndex(item => item.BookId == Id);
      if (LibBooks[BookIndex].VisitorId != 0)
      {
        /*If book is taken throw exception */
        throw new LibraryException("Book is taken.");
      } else {
        LibBooks.RemoveAt(BookIndex);
      }
    }

    /* Removing visitor from visitor's list function by id */
    public void DeleteVisitor(int Id)
    {
      int VisitorIndex = LibVisitors.FindIndex(item => item.VisitorId == Id);
      if (LibVisitors[VisitorIndex].Books.Count > 0)
      {
        /*If visitor hasn't returned book throw exception */
        throw new LibraryException("Visitor has taken some books and can't be deleted.");
      }
      else
      {
        LibVisitors.RemoveAt(VisitorIndex);
      }
    }

    /* Taking book function */
    public void TakeBook(int IdBook, int IdVisitor)
    {
      /* Find books by name */
      int BookIndex = LibBooks.FindIndex(item => item.BookId == IdBook);
      Book TempBook = LibBooks[BookIndex];

      /* Check if book is alredy taken */
      if (TempBook.VisitorId != 0)
      {
        string msg = "Book \"" + TempBook.Name + "\" alredy is taken";
        throw new LibraryException(msg);
      }

      /* Find visitor by name */
      int VisitorIndex = LibVisitors.FindIndex(item => item.VisitorId == IdVisitor);
      Visitor TempVisitor = LibVisitors[VisitorIndex];

      /* Check if visitor has taken less then 3 books */
      if (TempVisitor.Books.Count < 3)
      {
        /* Add note that book is taken */
        /* Change Book Object */
        TempBook.VisitorId = TempVisitor.VisitorId;
        TempBook.Term = DateTime.Now.AddDays(30);

        /* Change Visitor Object */
        TempVisitor.Books.Add(TempBook);

        /* Save changes */
        LibBooks[BookIndex] = TempBook;
        LibVisitors[VisitorIndex] = TempVisitor;
      }
      else
      {
        string msg = "Visitor \""+TempVisitor.Name + "\" alredy has taken 3 books.";
        throw new LibraryException(msg);
      }
    }

    /* Returning book function */
    public void ReturnBook(int IdBook, int IdVisitor)
    {
      /* Find books by name */
      int BookIndex = LibBooks.FindIndex(item => item.BookId == IdBook);
      Book TempBook = LibBooks[BookIndex];

      /* Find visitor by name */
      int VisitorIndex = LibVisitors.FindIndex(item => item.VisitorId == IdVisitor);
      Visitor TempVisitor = LibVisitors[VisitorIndex];

      /* Check if visitor has taken some books */
      if (TempVisitor.Books.Count > 0)
      {
        /* Find book that is returned */
        int IndexInVisitorsList = TempVisitor.Books.FindIndex(item => item.BookId == IdBook);
        if (IndexInVisitorsList >= 0 && IndexInVisitorsList < 3)
        {
          /* If book is found change book and Visitor Objects */
          TempBook.VisitorId = 0;
          TempBook.Term = DateTime.MinValue;

          TempVisitor.Books.RemoveAt(IndexInVisitorsList);

          /* Save changes */
          LibBooks[BookIndex] = TempBook;
          LibVisitors[VisitorIndex] = TempVisitor;
        }
      }
    }

    /* Getting list of books function */
    public List<Book> GetBooksList()
    {
      return LibBooks;
    }

    /* Getting list of visitors function */
    public List<Visitor> GetVisitorsList()
    {
      return LibVisitors;
    }

    /* Getting list of overdue books function */
    public List<Book> GetOverdueBooksList()
    {
      return LibBooks.Where(item => item.Term < DateTime.Now &&
                                    item.Term != DateTime.MinValue).ToList();
    }
    
    /* Getting books by name function */
    public List<Book> GetBooksByName(string Name, bool IfTaken)
    {
      List<Book> BooksList;
      
      /* If IfTaken == false return list of available books function */
      if (IfTaken == false)
      {
        BooksList = LibBooks.Where(item => item.Name == Name && item.VisitorId == 0).ToList();
      }
      else
      {
        BooksList = LibBooks.Where(item => item.Name == Name).ToList();
      }
      return BooksList;
    }

    /* Getting list of books taken by visitor function */
    public List<Book> GetBooksByName(string Name, int VisitorId)
    {
      return LibBooks.Where(item => item.Name == Name &&
                                    item.VisitorId == VisitorId).ToList();
    }
    
    /* Getting list of visitors  with name */
    public List<Visitor> GetVisitorsByName(string Name, bool IfCheckCanTakeBook)
    {
      /* Return list of visitors that can take book */
      if (IfCheckCanTakeBook == true)
      {
        return LibVisitors.Where(item => item.Name == Name &&
                                         item.Books.Count < 3).ToList();
      }
      else
      {
        return LibVisitors.Where(item => item.Name == Name).ToList();
      }
    }

    /* Getting list of visitors has taken book by name function */
    public List<Visitor> GetVisitorsByName(string VisitorName, string BookName)
    {
      List<Visitor> ReturnList = new List<Visitor>();
      
      /* List of visitors with name "VisitorName" */
      List<Visitor> VisitorsList = LibVisitors.Where(item => item.Name == VisitorName).ToList();
      foreach (Visitor Visitor in VisitorsList)
      {
        /* If Visitor has taken book "BookName" add him to ReturnList */
        if (Visitor.Books.FindIndex(book => book.Name == BookName) != -1)
        {
          ReturnList.Add(Visitor); 
        }
      }

      return ReturnList;
    }
  }
}
