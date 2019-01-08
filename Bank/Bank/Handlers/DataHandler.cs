using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;
using Newtonsoft.Json;

namespace Bank.Handlers
{
    public class DataHandler
    {
        public static string pathBankCodes = "BankCodes.json";

        public static Dictionary<int, string> GetBankCodes()
        {
            if (File.Exists(pathBankCodes))
            {
                string json = File.ReadAllText(pathBankCodes);
                return JsonConvert.DeserializeObject<Dictionary<int, string>>(json);
            }

            return null;
        }
    }
}
