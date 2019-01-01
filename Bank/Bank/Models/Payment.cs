using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int FromAccount { get; set; }
        public int FromBank { get; set; }
        public string DestAccount { get; set; }
        public string DestBank { get; set; }

        public DateTime Date { get; set; }

        public int Constant { get; set; }
        public int Variable { get; set; }
        public int Specific { get; set; }

        public string Message { get; set; }





    }
}
