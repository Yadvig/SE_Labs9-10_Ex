﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SE.Models
{
    public class AccountContext: DbContext
    {
        public AccountContext(): base("AccountDB") { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<DemandAccount> DemandAccounts { get; set; }
        public DbSet<DepositAccount> DepositAccounts { get; set; }
    }
}