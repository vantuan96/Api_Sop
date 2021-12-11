using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP.Shared.Models
{
    public class MessageReport
    {
        public bool Succeeded { get; set; } = false;

        public string Message { get; set; }

        public Exception Exception { get; set; }
    }
}
