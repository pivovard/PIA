using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public partial class BankContext
    {
        public async Task<bool> MakePayment(User user, Payment pay)
        {
            if(user.Money >= pay.Amount)
            {
                pay.UserId = user.Id;
                pay.FromAccount = (long)user.AccountNumber;
                user.Money -= pay.Amount;

                try
                {
                    this.Add(pay);
                    this.Update(user);
                    await this.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTemplateNameUnique(string name, int uid)
        {
            return !this.Template.Any(a => a.Name == name && a.UserId == uid);
        }


    }
}
