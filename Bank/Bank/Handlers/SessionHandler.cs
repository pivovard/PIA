using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Handlers
{

    public class SessionHandler
    {
        private static Dictionary<string, User> sessions = new Dictionary<string, User>();

        public static string NewSession(User u)
        {
            string g = Guid.NewGuid().ToString();
            sessions.Add(g, u);
            return g;
        }

        public static bool GetUser(string g, out User u)
        {
            return sessions.TryGetValue(g, out u);
        }

        public static void SetUser(string g, User u)
        {
            sessions[g] = u;
        }

        public static void DestroySession(string g)
        {
            sessions.Remove(g);
        }
    }
}
