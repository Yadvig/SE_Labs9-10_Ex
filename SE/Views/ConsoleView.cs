using System;
using SE.Controllers;

namespace SE.Views
{
    class ConsoleView
    {
        private BankManager manager;
        public ConsoleView(BankManager Manager)
        {
            manager = Manager;
        }
        public void ViewEngine()
        {
            bool alive = true;
            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1. Открыть счет \t 2. Вывести средства  \t 3. Добавить на счет");
                Console.WriteLine("4. Закрыть счет \t 5. Пропустить день \t 6. Список счетов");
                Console.WriteLine("7. Выйти из программы");
                Console.WriteLine("Введите номер пункта:");
                Console.ForegroundColor = color;
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());

                    switch (command)
                    {
                        case 1:
                            manager.OpenAccount();
                            break;
                        case 2:
                            manager.Withdraw();
                            break;
                        case 3:
                            manager.Put();
                            break;
                        case 4:
                            manager.CloseAccount();
                            break;
                        case 5:
                            manager.CalculatePercentage();
                            break;
                        case 6:
                            manager.DisplayAccounts();
                            break;
                        case 7:
                            alive = false;
                            continue;
                    }
                }
               catch (Exception ex)
                {
                    // выводим сообщение об ошибке красным цветом
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }
    }
}
