using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
  interface IDataBase
  {
    List<Book> GetListOfBooks();
    List<Visitor> GetListOfVisitors();
  }

  /* 3 book sources */
  class SQLLibSource : IDataBase
  {
    private List<Book> LibBooks; //List of books in library
    private List<Visitor> LibVisitors; //List of visitors in library

    public List<Book> GetListOfBooks()
    {
      return LibBooks;
    }

    public List<Visitor> GetListOfVisitors()
    {
      return LibVisitors;
    }

    public SQLLibSource()
    {
      LibBooks = new List<Book>();
      LibVisitors = new List<Visitor>();

      Book b1 = new Book("Pushkin", 1);
      Book b2 = new Book("Lermontov", 2);

      LibBooks.Add(b2);

      Visitor v1 = new Visitor("Alice", 1);
      b1.VisitorId = 1;
      b1.Term = new DateTime(2016, 12, 24);
      LibBooks.Add(b1);
      v1.Books.Add(b1);

      Visitor v2 = new Visitor("Bob", 2);
      LibVisitors.Add(v1);
      LibVisitors.Add(v2);
      
    }

  }

  class TXTLibSource : IDataBase
  {
    private List<Book> LibBooks; //List of books in library
    private List<Visitor> LibVisitors; //List of visitors in library

    public List<Book> GetListOfBooks()
    {
      return LibBooks;
    }

    public List<Visitor> GetListOfVisitors()
    {
      return LibVisitors;
    }

    public TXTLibSource()
    {
      LibBooks = new List<Book>();
      LibVisitors = new List<Visitor>();

      Book b1 = new Book("Krylov", 1);
      Book b2 = new Book("Olesha", 2);

      

      Visitor v1 = new Visitor("Dilan", 1);
      LibVisitors.Add(v1);
      

      Visitor v2 = new Visitor("White", 2);
      b2.VisitorId = 2;
      b2.Term = new DateTime(2016, 10, 5);
      v2.Books.Add(b2);
      LibVisitors.Add(v2);

      LibBooks.Add(b1);
      LibBooks.Add(b2);
    }
  }

  class DBLibSource : IDataBase
  {
    private List<Book> LibBooks; //List of books in library
    private List<Visitor> LibVisitors; //List of visitors in library

    public List<Book> GetListOfBooks()
    {
      return LibBooks;
    }

    public List<Visitor> GetListOfVisitors()
    {
      return LibVisitors;
    }

    public DBLibSource()
    {
      LibBooks = new List<Book>();
      LibVisitors = new List<Visitor>();

      Book b1 = new Book("Dog", 1);
      Book b2 = new Book("Cat", 2);


      Visitor v1 = new Visitor("Liza", 1);
      b1.VisitorId = 1;
      b1.Term = new DateTime(2016, 12, 21);
      v1.Books.Add(b1);
      LibVisitors.Add(v1);


      Visitor v2 = new Visitor("Mary", 2);
      b2.VisitorId = 2;
      b2.Term = new DateTime(2016, 12, 15);
      v2.Books.Add(b2);
      LibVisitors.Add(v2);

      LibBooks.Add(b1);
      LibBooks.Add(b2);
    }
  }
}
