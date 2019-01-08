using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bank.Models;

namespace Bank.Models
{
    public partial class BankContext : DbContext
    {
        public DbSet<Bank.Models.Payment> Payment { get; set; }

        public DbSet<Bank.Models.Template> Template { get; set; }

        public DbSet<Bank.Models.Standing> Standing { get; set; }

        public DbSet<Bank.Models.User> User { get; set; }


        public BankContext(DbContextOptions<BankContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity => {
                entity.HasIndex(e => e.Login).IsUnique();
                //entity.HasIndex(e => e.AccountNumber).IsUnique();
                //entity.HasIndex(e => e.CardNumber).IsUnique();
            });
        }
    }
}
