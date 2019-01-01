using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class Template
    {
        public int Id { get; set; }
        
        public int Account { get; set; }
        public int Bank { get; set; }

        public int Constant { get; set; }
        public int Variable { get; set; }
        public int Specific { get; set; }

        public string Message { get; set; }
    }
}
