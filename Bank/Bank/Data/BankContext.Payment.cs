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
            if (user.Money >= pay.Amount)
            {
                try
                {
                    pay.UserId = user.Id;
                    pay.FromAccount = (long)user.AccountNumber;
                    user.Money -= pay.Amount;

                    if (pay.DestBank == 666 && AccountExists(pay.DestAccount))
                    {
                        User u = this.User.FirstOrDefault(a => a.AccountNumber == pay.DestAccount);
                        u.Money += pay.Amount;

                        this.Update(u);
                        //await this.SaveChangesAsync();
                    }
                    
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

        public bool AccountExists(long acc)
        {
            return this.User.Any(a => a.AccountNumber == acc);
        }

        public bool IsTemplateNameUnique(string name, int uid)
        {
            return !this.Template.Any(a => a.Name == name && a.UserId == uid);
        }


    }
}
