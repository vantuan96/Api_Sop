using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP.ComService
{
    public static class StaticFields
    {
        public static bool isLogOut = false;
        public static bool isAutorun = false;
        public static string APIURL = "";
        public static string Username = "";
        public static string Password = "";
        public static int UserId = 0;
        public static Dictionary<string, int> RatingValue = new Dictionary<string, int>()
        {
            { "a5b", 5 },
            { "c4d", 4 },
            { "e3f", 3 },
            { "g2h", 2 },
            { "i1k", 1 },
        };
    }
}
