using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_SOP.LibaryHelper
{
    public class MessageReport
    {
        public bool isSuccess { get; set; }

        public string Message { get; set; }

        public MessageReport()
        {

        }

        public MessageReport(bool isSuccess, string Message)
        {
            this.isSuccess = isSuccess;
            this.Message = Message;
        }
    }
    public class MessageReportCustom
    {
        public bool isSuccess { get; set; }

        public string Message { get; set; }
        public string CardNumber { get; set; }
    }
}

