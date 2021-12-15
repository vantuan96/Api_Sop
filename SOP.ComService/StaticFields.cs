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
        public static Dictionary<string, int> RatingValue = new Dictionary<string, int>()
        {
            { "a", 5 },
            { "b", 4 },
            { "c", 3 },
            { "d", 2 },
            { "e", 1 },
        };
    }
}
