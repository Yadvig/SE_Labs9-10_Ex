using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SE.Models;
using SE.Controllers;
using SE.Views;

namespace SE
{
    class Program
    {
        static void Main(string[] args)
        {

            BankManager manager = new BankManager(new Bank<Account>("SomeBank"));
            ConsoleView view = new ConsoleView(manager);
            view.ViewEngine();
           
        }
       
    }
}
