using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
  class LibraryView
  {
    public string Command; //CoomandLine input

    public delegate void UI();
    public event UI NewCommand; //New Command Event
    public void OnGettingNewCommand()
    {
      NewCommand();
    }

    /* Print help function */
    public void PrintHelp()
    {
      string HelpMessage = "MVC Homework: library application\n";
      HelpMessage+="Supported commands:\n";
      HelpMessage += "+book \"Book name\" — add book to library\n";
      HelpMessage += "-book \"Book name\" — remove book from library\n";
      HelpMessage += "+visitor \"Visitor name\" — add visitor to library\n";
      HelpMessage += "-visitor \"Visitor name\" — remove visitor from library\n";
      HelpMessage += "books — print book's list\n";
      HelpMessage += "visitors — print visitor's list\n";
      HelpMessage += "take \"Book name\" \"Visitor name\" — take book from library\n";
      HelpMessage += "return \"Book name\" \"Visitor name\" — return book\n";
      HelpMessage += "overdue — print list of books that must have been returned after 30 days\n";
      HelpMessage += "help — print help\n";
      HelpMessage += "exit — close application\n";
      Console.WriteLine(HelpMessage);
    }

    /* Print Error Function */
    public void PrintError(string Error)
    {
      Console.WriteLine("Error: " + Error);
    }

    /* Print Message Function */
    public void Print(string Message)
    {
      Console.WriteLine(Message);
    }

    /* Reading Commands Function */
    public void ReadCommands()
    {
      /* Reading commands while "exit" do not come */
      Command = "";
      while (String.Compare(Command, "exit", true) != 0)
      {
        Command = Console.ReadLine();
        
        /*Send "New command" Event */
        NewCommand();
      }
    }

    /* Reading ID from Console function */
    public string RequestId()
    {
      return Console.ReadLine();
    }
  }
}
