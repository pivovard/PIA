using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class Users
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int BirthNumber { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }

        public int AccountNumber { get; set; }
        public int CardNumber { get; set; }

        public int  Login { get; set; }
        public int Password { get; set; }
        public Role Role { get; set; }



    }
}
