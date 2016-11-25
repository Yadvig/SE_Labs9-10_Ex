using System.Collections.Generic;
using System.Linq;

namespace SE.Models
{
    public enum AccountType
    {
        Ordinary,
        Deposit
    }
    public class Bank
    {
        AccountContext db;
        private Account account = null;

        public string Name { get; private set; }

        public Bank(string name)
        {
            this.Name = name;
            db = new AccountContext();
           
        }
        //подключение обработчиков из контроллера к событиям модели
        public void Attach(AccountStateHandler addSumHandler, AccountStateHandler withdrawSumHandler,
            AccountStateHandler calculationHandler, AccountStateHandler closeAccountHandler,
            AccountStateHandler openAccountHandler)
        {
            Account.Added += addSumHandler;
            Account.Withdrawed += withdrawSumHandler;
            Account.Closed += closeAccountHandler;
            Account.Opened += openAccountHandler;
            Account.Calculated += calculationHandler;
        }
        // метод создания счета
        public void Open(AccountType accountType, decimal sum)
        {
                //выбор типа счета и создание объекта конкретного счета
                switch (accountType)
                {
                    case AccountType.Ordinary:
                        account = new DemandAccount(sum, 1);
                        db.DemandAccounts.Add(account as DemandAccount);
                        break;
                    case AccountType.Deposit:
                        account = new DepositAccount(sum, 40);
                        db.DepositAccounts.Add(account as DepositAccount);
                        break;                                             
                }
                //добавление нового счета в базу данных
                db.SaveChanges();
                account.Open();
        }
        //добавление средств на счет
        public void Put(decimal sum, int id)
        {
               account= db.Accounts.Find(id);
               account.Put(sum);
               db.SaveChanges();
        }
        // вывод средств
        public void Withdraw(decimal sum, int id)
        {
                account = db.Accounts.Find(id);
                account.Withdraw(sum);
                db.SaveChanges();
        }
        // закрытие счета
        public void Close(int id)
        {
                account = db.Accounts.Find(id);
                account.Close();
                db.Accounts.Remove(account);
                db.SaveChanges();

        }
        // начисление процентов по счетам
        public void CalculatePercentage()
        {
                foreach (Account account in db.Accounts)
                {
                    account.IncrementDays();
                    account.Calculate();
                }
                db.SaveChanges();
        }
        public List<Account> ViewAccounts()
        {
            List<Account> accounts = db.Accounts.ToList();
            return accounts;
        }
    }
}
