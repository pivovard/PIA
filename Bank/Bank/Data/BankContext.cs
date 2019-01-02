using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bank.Models;

namespace Bank.Models
{
    public class BankContext : DbContext
    {
        public BankContext (DbContextOptions<BankContext> options)
            : base(options)
        {
        }


        public DbSet<Bank.Models.Payment> Payment { get; set; }

        public DbSet<Bank.Models.Template> Template { get; set; }

        public DbSet<Bank.Models.Standing> Standing { get; set; }

        public DbSet<Bank.Models.User> User { get; set; }
    }
}
