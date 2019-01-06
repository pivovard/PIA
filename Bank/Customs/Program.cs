using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Customs
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] s;
            using (var sha = new SHA256Managed())
            {
                s = sha.ComputeHash(BitConverter.GetBytes(123));
            }
            Console.WriteLine(s.ToArray());
            Console.ReadKey();
        }
    }
}
