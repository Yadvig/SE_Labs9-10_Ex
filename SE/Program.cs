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
            //Создание контроллера и инициализация его моделью
            BankManager manager = new BankManager(new Bank("SomeBank"));
            //Создание представление и инициализация его контроллером
            ConsoleView view = new ConsoleView(manager);
            //Запуск представления
            view.ViewEngine();
           
        }
       
    }
}
