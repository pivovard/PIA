using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Handlers
{
    public class TransactionHandler
    {
        private static Dictionary<long, string> transactions = new Dictionary<long, string>();

        private static int counter = 0;
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random r = new Random();


        public static int NewAuth(User user)
        {
            counter++;

            string code = new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[r.Next(s.Length)]).ToArray());

            transactions.Add(counter, code);

            MailClient.Singleton.SendAuth(user, code);

            return counter;
        }

        public static int NewPayment(User user, Payment pay)
        {
            counter++;

            string code = new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[r.Next(s.Length)]).ToArray());

            transactions.Add(counter, code);

            MailClient.Singleton.SendPayment(user, pay, code);

            return counter;
        }

        public static bool IsValid(int t, string code)
        {
            if(transactions.ContainsKey(t) && transactions[t].Equals(code))
            {
                transactions.Remove(t);
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
