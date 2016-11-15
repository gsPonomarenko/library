using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
  struct Visitor
  {
    public string Name; //Visitor name
    public int VisitorId; // Visitor ID
    public List<Book> Books; //List of books Visitor has taken

    public Visitor(string Name, int Id)
    { 
      /*Default Constructor*/
      this.Name = Name;
      this.VisitorId = Id;
      this.Books = new List<Book>();
    }
  }
}
