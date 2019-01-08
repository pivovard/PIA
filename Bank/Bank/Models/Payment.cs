using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public long FromAccount { get; set; }
        public int FromBank { get; } = 666;

        public long? DestAccountPrefix { get; set; }
        [Required]
        public long DestAccount { get; set; }
        [Required]
        public int DestBank { get; set; }

        [Required]
        public double Amount { get; set; }

        public int? Constant { get; set; }
        public int? Variable { get; set; }
        public int? Specific { get; set; }

        public string Message { get; set; }

        public Payment() { }
        public Payment(Template template)
        {
            this.DestAccountPrefix = template.DestAccountPrefix;
            this.DestAccount = template.DestAccount;
            this.DestBank = template.DestBank;
            this.Amount = template.Amount;
            this.Constant = template.Constant;
            this.Variable = template.Variable;
            this.Specific = template.Specific;
        }

    }
}
