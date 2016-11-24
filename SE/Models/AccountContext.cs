using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SE.Models
{
    class AccountContext: DbContext
    {
        public AccountContext(): base ("DbConnetcion") { }

        public DbSet<Account> Accounts { get; set; }
    }
}
