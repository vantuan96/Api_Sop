using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP.ComService
{
    public class PortInfoModel
    {
        public int Index { get; set; }

        public int UserId { get; set; } = 0;

        public string UserName { get; set; } = "";

        public string ComPort { get; set; } = "";

        public bool AutoStart { get; set; } = false;
    }
}
