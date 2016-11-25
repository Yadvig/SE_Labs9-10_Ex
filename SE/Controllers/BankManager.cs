﻿using System;
using System.Collections.Generic;
using SE.Models;

namespace SE.Controllers
{
    public class BankManager
    {
        private Bank bank;
        public BankManager(Bank Bank)
        {
            bank = Bank;
        }
        //связывание обработчиков событий в контроллере и самих событий в модели
        public void AttachEvents()
        {
            bank.Attach(
               AddSumHandler,  // обработчик добавления средств на счет
               WithdrawSumHandler, // обработчик вывода средств
               (o, e) => Console.WriteLine(e.Message),  // обработчик начислений процентов в виде лямбда-выражения
               CloseAccountHandler, // обработчик закрытия счета
               OpenAccountHandler); // обработчик открытия счета
        }
        public void OpenAccount()
        {
            Console.WriteLine("Укажите сумму для создания счета:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Выберите тип счета: 1. До востребования 2. Депозит");
            AccountType accountType;

            int type = Convert.ToInt32(Console.ReadLine());

            if (type == 2)
                accountType = AccountType.Deposit;
            else
                accountType = AccountType.Ordinary;

            bank.Open(accountType, sum);
        
        }

        public void Withdraw()
        {
            Console.WriteLine("Укажите сумму для вывода со счета:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Введите id счета:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Withdraw(sum, id);
        }

        public void Put()
        {
            Console.WriteLine("Укажите сумму, чтобы положить на счет:");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Введите Id счета:");
            int id = Convert.ToInt32(Console.ReadLine());
            bank.Put(sum, id);
        }

        public void CloseAccount()
        {
            Console.WriteLine("Введите id счета, который надо закрыть:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Close(id);
        }
        public void CalculatePercentage()
        {
            bank.CalculatePercentage();
        }
        public void DisplayAccounts()
        {
            List<Account> accounts = bank.ViewAccounts();
            foreach (Account account in accounts)
            {
                Console.WriteLine("Счет № {0} - Ставка {1}% - Сумма {2} - Дней {3}", account.Id, account.Percentage, account.Sum, account.Days);
            }
        }
        // обработчики событий класса Account
        // обработчик открытия счета
        private static void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        // обработчик добавления денег на счет
        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        // обработчик вывода средств
        private static void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        // обработчик закрытия счета
        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
