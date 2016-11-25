namespace SE.Models
{
    public abstract class Account: IAccount
    {
        //Событие, возникающее при выводе денег
        protected internal static event AccountStateHandler Withdrawed;
        // Событие возникающее при добавление на счет
        protected internal static event AccountStateHandler Added;
        // Событие возникающее при открытии счета
        protected internal static event AccountStateHandler Opened;
        // Событие возникающее при закрытии счета
        protected internal static event AccountStateHandler Closed;
        // Событие возникающее при начислении процентов
        protected internal static event AccountStateHandler Calculated;

        public int Id { get; set; }
       
        public decimal Sum { get; set; } // Переменная для хранения суммы

        public int Percentage { get; set; } // Переменная для хранения процента

        public int Days { get; set; } = 0;// время с момента открытия счета

        public Account(decimal sum, int percentage)
        {
            Sum = sum;
            Percentage = percentage;           
        }

        public Account() { }

        // вызов событий
        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if (handler != null && e != null)
                handler(this, e);
        }
        // вызов отдельных событий. Для каждого события определяется свой витуальный метод
        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }
        protected virtual void OnWithdrawed(AccountEventArgs e)
        {
            CallEvent(e, Withdrawed);
        }
        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }
        protected virtual void OnCalculated(AccountEventArgs e)
        {
            CallEvent(e, Calculated);
        }
        //Операции со счетом
        public virtual void Put(decimal sum)
        {
            Sum += sum;
            OnAdded(new AccountEventArgs("На счет поступило " + sum, sum));
        }
        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (sum <= Sum)
            {
                Sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEventArgs("Сумма " + sum + " снята со счета " + Id, sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs("Недостаточно денег на счете " + Id, sum));
            }
            return result;
        }
        // открытие счета
        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs("Открыт новый депозитный счет!Id счета: " + this.Id, this.Sum));
        }
        // закрытие счета
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs("Счет " + Id + " закрыт.  Итоговая сумма: " + Sum, Sum));
        }

        protected internal void IncrementDays()
        {
            Days++;
        }
        // начисление процентов
        protected internal virtual void Calculate()
        {
            decimal increment = Sum * Percentage / 100;
            Sum = Sum + increment;
            OnCalculated(new AccountEventArgs("Начислены проценты в размере: " + increment, increment));
        }
    }
}
