using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
  [Serializable]
  public class LibraryException : Exception
  {
    /*Exception in 'library' application*/
    
    /*Default Constructor*/
    public LibraryException()
    { }

    /*Constructor with Error Message*/
    public LibraryException(string message)
      : base(message)
    { }

    
    public LibraryException(string message, Exception innerException)
      : base(message, innerException)
    { }

    
    protected LibraryException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext contex)
            : base(info, contex) { }
  }
}
