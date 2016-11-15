using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
  struct Book
  {
    public string Name; //Book name
    public int BookId; //Book ID
    public int VisitorId; //ID of Visitor that has taken book
    public DateTime Term; //Date when book must be returned

    public Book(string Name, int Id)
    {
      /*Default Constructor*/
      this.Name = Name;
      this.BookId = Id;
      this.VisitorId = 0;
      this.Term = new DateTime();
    }
  }
}
