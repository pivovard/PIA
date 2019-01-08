using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public partial class BankContext
    {
        public void GenerateLogin(User user)
        {
            Random r = new Random();
            string login;

            do
            {
                login = r.Next(10000000, 100000000).ToString();
            } while (this.User.Any(a => a.Login == login));

            user.Login = login;
            user.Pin = user.HashPin(r.Next(1000, 10000));
        }

        public long GenerateAccountNumber()
        {
            Random r = new Random();
            long n;

            do
            {
                n = r.Next(10000000, 100000000) * 100000000L + r.Next(10000000, 100000000);
            } while (this.User.Any(a => a.AccountNumber == n));

            return n;
        }
        public long GenerateCardNumber()
        {
            Random r = new Random();
            long n;

            do
            {
                n = r.Next(10000000, 100000000) * 100000000L + r.Next(10000000, 100000000);
            } while (this.User.Any(a => a.CardNumber == n));

            return n;
        }

        public bool IsAccountUnique(long acc)
        {
            return !this.User.Any(a => a.AccountNumber == acc);
        }

        public bool IsCardUnique(long card)
        {
            return !this.User.Any(a => a.CardNumber == card);
        }
    }
}
