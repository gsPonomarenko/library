using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
  class Program
  {
    static void Main(string[] args)
    {
      /* Initializing Library Controller */
      LibraryController LibControl = new LibraryController();
      
      /* Runninng Controller */
      LibControl.Run();
    }
  }
}
