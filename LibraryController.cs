using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
  class LibraryController
  {
    private LibraryModel Model; //Model Object (MVC Pattern)
    private LibraryView View; //View Object (MVC Pattern)
    private string Msg; //String for messages

    /* Default constructor */
    public LibraryController()
    {
      View = new LibraryView();
      Model = new LibraryModel();
    }

    /* Main Controller function */
    public void Run()
    {
      /* Subscribing for event "New Command" */
      View.NewCommand += this.ProcessCommand;
      
      /* Reading commands in loop */
      View.ReadCommands();
      
      /* Unsubscribing from event "New Command" */
      View.NewCommand -= this.ProcessCommand;
    }

    /* Processing commands function  */
    public void ProcessCommand()
    {
      /* Split command and arguments */
      string[] Command = View.Command.Split('\"');
      Command[0] = Command[0].Split(' ').First();

      /* Find command and run function */
      switch(Command[0]) {
        case "+book":
          AddBook(Command);
          break;
        case "-book":
          DeleteBook(Command);
          break;
        case "+visitor":
          AddVisitor(Command);
          break;
        case "-visitor":
          DeleteVisitor(Command);
          break;
        case "books":
          PrintBooksList();
          break;
        case "visitors":
          PrintVisitorsList();
          break;
        case "overdue":
          PrintOverdueBooksList();
          break;
        case "take":
          TakeBook(Command);
          break;
        case "return":
          ReturnBook(Command);
          break;
        case "help":
          Help();
          break;
        case "exit":
          break;
        default:
          View.PrintError("Unknown command");
          break;
      }
    }

    /* Adding book function */
    private void AddBook(string[] Command)
    {
      string BookName = "";

      /* If command input is correct add book */
      if (Command.Length == 3)
      {
        BookName = Command[1];
        Model.AddBook(BookName);
      }
      else
      {
        View.PrintError("Invalid argument. Books's name must stay between quotes (\"name\").");
      }
    }

    /* Deleting book function */
    private void DeleteBook(string[] Command)
    {
      int IdBook = 0;
      string BookName = "";

      /* If command input is correct delete book */
      if (Command.Length == 3)
      {
        BookName = Command[1];
        try
        {
          /* Try to get book id by name and delete book */
          IdBook = GetIdByBookName(BookName, true);
          Model.DeleteBook(IdBook);
        }
        catch (LibraryException ex)
        {
          View.PrintError(ex.Message);
        }
      }
      else
      {
        View.PrintError("Invalid argument. Books's name must stay between quotes (\"name\").");
      }
    }

    /* Adding visitor function */
    private void AddVisitor(string[] Command)
    {
      string VisitorName = "";

      /* If command input is correct add visitor */
      if (Command.Length == 3)
      {
        VisitorName = Command[1];
        Model.AddVisitor(VisitorName);
      }
      else
      {
        View.PrintError("Invalid argument. Visitor's name must stay between quotes (\"name\").");
      }
    }
    
    /* Deleting visitor function */
    private void DeleteVisitor(string[] Command)
    {
      int IdVisitor = 0;
      string VisitorName = "";

      /* If command input is correct delete visitor */
      if (Command.Length == 3)
      {
        IdVisitor = 0;
        VisitorName = Command[1];
        try
        {
          /* Try to get visitor id by name and delete visitor */
          IdVisitor = GetIdByVisitorName(VisitorName, false);
          Model.DeleteVisitor(IdVisitor);
        }
        catch (LibraryException Ex)
        {
          View.PrintError(Ex.Message);
        }
      }
      else
      {
        View.PrintError("Invalid argument. Visitor's name must stay between quotes (\"name\").");
      }
    }

    /* Printing book's list function */
    private void PrintBooksList()
    {
      /* Get list of all books */
      List<Book> BooksList = Model.GetBooksList();
      /* If list isn't empty print it */
      if (BooksList.Count > 0)
      {
        /* Form a massage and send it to view */
        Msg = "ID\tName\tVisitor ID\tReturn date\n";
        foreach (Book Book in BooksList)
        {
          Msg += Book.BookId.ToString() + "\t";
          Msg += Book.Name + "\t";
          if (Book.VisitorId != 0)
          {
            Msg += Book.VisitorId.ToString() + "\t\t";
          }
          else
          {
            Msg += "-\t\t";
          }

          if (Book.Term != DateTime.MinValue)
          {
            Msg += Book.Term.ToShortDateString() + "\n";
          }
          else
          {
            Msg += "-\n";
          }
        }
      }
      else
      {
        Msg = "Book's list is empty.";
      }
      View.Print(Msg);
    }

    /* Printing visitors's list function */
    private void PrintVisitorsList()
    {
      /* Get list of all visitors */
      List<Visitor> visitors = Model.GetVisitorsList();
      /* If list isn't empty print it */
      if (visitors.Count > 0)
      {
        /* Form a massage and send it to view */
        Msg = "ID\tName\tBook ID\tBook name\tTerm\n";
        foreach (Visitor visitor in visitors)
        {
          Msg += visitor.VisitorId.ToString() + "\t";
          Msg += visitor.Name + "\t";
          if (visitor.Books.Count > 0)
          {
            int BooksIterator = 0;
            foreach (Book Book in visitor.Books)
            {
              Msg += Book.BookId.ToString() + "\t";
              Msg += Book.Name + "\t\t";
              Msg += Book.Term.ToShortDateString() + "\n";
              BooksIterator++;
              if (visitor.Books.Count > 1 && BooksIterator != visitor.Books.Count)
              {
                Msg += "\t\t";
              }
            }
          }
          else
          {
            Msg += "-\t-\t\t-\n";
          }
        }
      }
      else
      {
        Msg = "Visitor's list is empty.";
      }
      View.Print(Msg);
    }

    /* Printing overdue book's list function */
    private void PrintOverdueBooksList()
    {
      /* Get list of overdue books */
      List<Book> BooksList = Model.GetOverdueBooksList();
      /* If list isn't empty print it */
      if (BooksList.Count > 0)
      {
        /* Form a massage and send it to view */
        Msg = "ID\tName\tVisitor ID\tReturn date\n";
        foreach (Book Book in BooksList)
        {
          Msg += Book.BookId.ToString() + "\t";
          Msg += Book.Name + "\t";
          if (Book.VisitorId != 0)
          {
            Msg += Book.VisitorId.ToString() + "\t\t";
          }
          else
          {
            Msg += "-\t\t";
          }

          if (Book.Term != DateTime.MinValue)
          {
            Msg += Book.Term.ToShortDateString() + "\n";
          }
          else
          {
            Msg += "-\n";
          }
        }
      }
      else
      {
        Msg = "Overdue book's list is empty.";
      }
      View.Print(Msg);
    }

    /* Taking book function */
    private void TakeBook(string[] Command)
    {
      int IdBook = 0;
      int IdVisitor = 0;
      string BookName = "";
      string VisitorName = "";

      /* If command input is correct take book */
      if (Command.Length == 5)
      {
        BookName = Command[1];
        VisitorName = Command[3];
        try
        {
          /* Get book's and visitor's ids by names */
          IdBook = GetIdByBookName(BookName, false);
          IdVisitor = GetIdByVisitorName(VisitorName, true);
          /* Take book in model */
          Model.TakeBook(IdBook, IdVisitor);
        }
        catch (LibraryException Ex)
        {
          View.PrintError(Ex.Message);
        }
      }
      else
      {
        View.PrintError("Invalid argument. Books's and visitor's name must stay between quotes (\"name\").");
      }
    }

    /* Returning book function */
    private void ReturnBook(string[] Command)
    {
      int[] Ids = new int[2] {0,0};
      string BookName = "";
      string VisitorName = "";

      /* If command input is correct return book */
      if (Command.Length == 5)
      {
        BookName = Command[1];
        VisitorName = Command[3];
        try
        {
          /* Get book's and visitor's ids by names */
          Ids = GetIdsByBookAndVisitorName(BookName, VisitorName);
          /* Return book in model */
          Model.ReturnBook(Ids[1], Ids[0]);
        }
        catch (LibraryException Ex)
        {
          View.PrintError(Ex.Message);
        }
      }
      else
      {
        View.PrintError("Invalid argument. Books's and visitor's name must stay between quotes (\"name\").");
      }
    }

    /* Getting visitor's id by name */
    private int GetIdByVisitorName(string name, bool IfCheckCanTakeBook)
    {
      int Id = 0;

      /* Get list of visitors by name */
      List<Visitor> VisitorsList = Model.GetVisitorsByName(name, IfCheckCanTakeBook);
      /* Check list isn't empty */
      if (VisitorsList.Count > 0)
      {
        /* Check list has more then one item */
        if (VisitorsList.Count > 1)
        {
          /* Print visitors with this name */
          View.Print("There are " + VisitorsList.Count + " visitors with name " + name);

          Msg = "ID\tName\n";
          foreach (Visitor Visitor in VisitorsList)
          {
            Msg += Visitor.VisitorId.ToString() + "\t";
            Msg += Visitor.Name + "\n";
          }
          View.Print(Msg);
          View.Print("Type id of visitor you need:");

          /* Get id from view */
          string response = View.RequestId();
          /* If id is correct return it */
          if (Int32.TryParse(response, out Id) == true)
          {
            int VisitorIndex = VisitorsList.FindIndex(item => item.VisitorId == Id);
            if (VisitorIndex != -1)
            {
              return Id;
            }
            else
            {
              throw new LibraryException("Incorrect Id");
            }
          }
          else
          {
            throw new LibraryException("Incorrect Id");
          }
        }
        else
        {
          return VisitorsList[0].VisitorId;
        }
      }
      else
      {
        throw new LibraryException("Visitor \"" + name + "\" don't exist.");
      }
    }

    private int[] GetIdsByBookAndVisitorName(string BookName, string VisitorName)
    {
      int[] Id = new int[2];

      /* Get list of visitors has taken book by name */
      List<Visitor> VisitorsList = Model.GetVisitorsByName(VisitorName, BookName);
      /* Check list isn't empty */
      if (VisitorsList.Count > 0)
      {
        /* Check list has more then one item */
        if (VisitorsList.Count > 1)
        {
          /* Print visitors with this name */
          View.Print("There are " + VisitorsList.Count + " visitors with name " + VisitorName);

          Msg = "ID\tName\n";
          foreach (Visitor Visitor in VisitorsList)
          {
            Msg += Visitor.VisitorId.ToString() + "\t";
            Msg += Visitor.Name + "\n";
          }
          View.Print(Msg);
          View.Print("Type id of visitor you need:");

          /* Get id from view */
          string response = View.RequestId();
          /* If visitor's id is correct try to get book's id */
          if (Int32.TryParse(response, out Id[0]) == true)
          {
            int VisitorIndex = VisitorsList.FindIndex(item => item.VisitorId == Id[0]);
            if (VisitorIndex != -1)
            {
              Id[1] = GetIdByBookName(BookName, Id[0]);
              return Id;
            }
            else
            {
              throw new LibraryException("Incorrect Id");
            }
          }
          else
          {
            throw new LibraryException("Incorrect Id");
          }
        }
        else
        {
          Id[0] = VisitorsList[0].VisitorId;
          Id[1] = GetIdByBookName(BookName, Id[0]);
          return Id;
        }
      }
      else
      {
        Msg = "Visitor \"" + VisitorName + "\" with book \"";
        Msg += BookName + "\" don't exist.";
        throw new LibraryException(Msg);
      }
    }

    /* Getting visitor's id by BookName and VisitorId */
    private int GetIdByBookName(string BookName, int VisitorId) {
      int Id = 0;

      /* Get list of books visitor has taken by name */
      List<Book> BooksList = Model.GetBooksByName(BookName, VisitorId);
      /* Check list isn't empty */
      if (BooksList.Count > 0)
      {
        /* Check list has more then one item */
        if (BooksList.Count > 1)
        {
          /* Print books with this name */
          View.Print("There are " + BooksList.Count + " books with name " + BookName);

          Msg = "ID\tName\n";
          foreach (Book book in BooksList)
          {
            Msg += book.BookId.ToString() + "\t";
            Msg += book.Name + "\n";
          }
          View.Print(Msg);
          View.Print("Type id of book you need:");

          /* Get id from view */
          string response = View.RequestId();
          /* If id is correct return it */
          if (Int32.TryParse(response, out Id) == true)
          {
            int BookId = BooksList.FindIndex(item => item.BookId == Id);
            if (BookId != -1)
            {
              return Id;
            }
            else
            {
              throw new LibraryException("Incorrect Id");
            }
          }
          else
          {
            throw new LibraryException("Incorrect Id");
          }
        }
        else
        {
          return BooksList[0].BookId;
        }

      }
      else
      {
        throw new LibraryException("Book \"" + BookName + "\" don't exist or didn't taken by visitor id "+ VisitorId);
      }
    }

    /* Getting books's id by name */
    private int GetIdByBookName(string name, bool IfTaken)
    {
      int Id = 0;

      /* Get list of books by name */
      List<Book> BooksList = Model.GetBooksByName(name, IfTaken);
      /* Check list isn't empty */
      if (BooksList.Count > 0)
      {
        /* Check list has more then one item */
        if (BooksList.Count > 1)
        {
          /* Print books with this name */
          View.Print("There are " + BooksList.Count + " books with name " + name);

          Msg = "ID\tName\n";
          foreach (Book book in BooksList)
          {
            Msg += book.BookId.ToString() + "\t";
            Msg += book.Name + "\n";
          }
          View.Print(Msg);
          View.Print("Type id of book you need:");

          /* Get id from view */
          string response = View.RequestId();
          /* If id is correct return it */
          if (Int32.TryParse(response, out Id) == true)
          {
            int BookId = BooksList.FindIndex(item => item.BookId == Id);
            if (BookId != -1)
            {
              return Id;
            }
            else
            {
              throw new LibraryException("Incorrect Id");
            }
          }
          else
          {
            throw new LibraryException("Incorrect Id");
          }
        }
        else
        {
          return BooksList[0].BookId;
        }

      }
      else
      {
        if (IfTaken == true)
        {
          throw new LibraryException("Book \"" + name + "\" doesn't exist.");
        }
        else
        {
          throw new LibraryException("Book \"" + name + "\" is taken or doesn't exist.");
        }
      }
    }

    /* Help function */
    private void Help()
    {
      View.PrintHelp();
    }
  }
}
