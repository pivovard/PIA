using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public int Phone { get; set; }

        [Required]
        public int AccountNumber { get; set; }
        [Required]
        [DataType(DataType.CreditCard)]
        public int CardNumber { get; set; }
        [Required]
        public int Money { get; set; }

        [Required]
        public int  Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public int Pin { get; set; }
        [Required]
        public Role Role { get; set; }



    }
}
