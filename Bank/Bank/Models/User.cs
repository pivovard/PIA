using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Bank.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int BirthNumber { get; set; }
        public string Adress { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int? Phone { get; set; }
        
        public long? AccountNumber { get; set; }
        public long? CardNumber { get; set; }
        public long? Money { get; set; }

        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Pin { get; set; }
        [Required]
        public Role Role { get; set; }

        public void GenerateLogin(BankContext context)
        {
            Random r = new Random();
            string login;

            do
            {
                login = r.Next(10000000, 100000000).ToString();
            } while (context.User.Any(a => a.Login == login));

            this.Login = login;
            this.Pin = HashPin(r.Next(1000, 10000));

            //docasny vypis
            Console.WriteLine($"{Login} - {Pin}");
        }

        public void GenerateAccountNumber(BankContext context)
        {
            Random r = new Random();
            long n;
            
            do
            {
                n = r.Next(10000000, 100000000) * 100000000L + r.Next(10000000, 100000000);
            } while (context.User.Any(a => a.AccountNumber == n));

            this.AccountNumber = n;
        }
        public void GenerateCardNumber(BankContext context)
        {
            Random r = new Random();
            long n;

            do
            {
                n = r.Next(10000000, 100000000) * 100000000L + r.Next(10000000, 100000000);
            } while (context.User.Any(a => a.CardNumber == n));

            this.CardNumber = n;
        }

        public bool IsAccountUnique(BankContext context)
        {
            return !context.User.Any(a => a.AccountNumber == this.AccountNumber);
        }

        public bool IsCardUnique(BankContext context)
        {
            return !context.User.Any(a => a.CardNumber == this.CardNumber);
        }

        public string HashPin(int pin)
        {
            byte[] hash;
            using (var sha = new SHA256Managed())
            {
                hash = sha.ComputeHash(BitConverter.GetBytes(pin));
            }
            return BitConverter.ToString(hash);
        }

    }
}
