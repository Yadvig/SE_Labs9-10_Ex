using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Models
{
    public enum AccountType
    {
        Ordinary,
        Deposit
    }
    public class Bank
    {
       AccountContext db = new AccountContext();
       public string Name { get; private set; }

        public Bank(string name)
        {
            this.Name = name;
        }
        // метод создания счета
        public void Open(AccountType accountType, decimal sum,
            AccountStateHandler addSumHandler, AccountStateHandler withdrawSumHandler,
            AccountStateHandler calculationHandler, AccountStateHandler closeAccountHandler,
            AccountStateHandler openAccountHandler)
        {
            using (AccountContext db = new AccountContext())
            {
                Account newAccount = null;
                //выбор типа счета и создание объекта конкретного счета
                switch (accountType)
                {
                    case AccountType.Ordinary:
                        newAccount = new DemandAccount(sum, 1);
                        db.DemandAccounts.Add(newAccount as DemandAccount);
                        break;
                    case AccountType.Deposit:
                        newAccount = new DepositAccount(sum, 40);
                        db.DepositAccounts.Add(newAccount as DepositAccount);
                        break;                                             
                }
                // установка обработчиков событий счета
                newAccount.Added += addSumHandler;
                newAccount.Withdrawed += withdrawSumHandler;
                newAccount.Closed += closeAccountHandler;
                newAccount.Opened += openAccountHandler;
                newAccount.Calculated += calculationHandler;
                //добавление нового счета в базу данных
                db.SaveChanges();
                newAccount.Open();
            }
        }
        //добавление средств на счет
        public void Put(decimal sum, int id)
        {
                Account account = db.Accounts.Find(id);
                account.Put(sum);
                db.SaveChanges();
        }
        // вывод средств
        public void Withdraw(decimal sum, int id)
        {
                Account account = db.Accounts.Find(id);
                account.Withdraw(sum);
                db.SaveChanges();
        }
        // закрытие счета
        public void Close(int id)
        {
                Account account = db.Accounts.Find(id);
                db.Accounts.Remove(account);
                db.SaveChanges();

        }
        // начисление процентов по счетам
        public void CalculatePercentage()
        {
                foreach (Account a in db.Accounts)
                {
                    a.IncrementDays();
                    a.Calculate();
                }
                db.SaveChanges();
        }
    }
}
