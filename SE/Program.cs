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
            BankManager manager = new BankManager(new Bank<Account>("SomeBank"));
            //Создание представление и инициализация его контроллером
            ConsoleView view = new ConsoleView(manager);
            //Запуск представления
            view.ViewEngine();
           
        }
       
    }
}
