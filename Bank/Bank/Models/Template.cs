using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class Template
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }

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
    }
}
