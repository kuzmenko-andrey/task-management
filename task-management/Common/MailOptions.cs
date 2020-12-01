using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task_management.Common
{
    public class MailOptions
    {
        public string ServerHost { get; set; }
        public int ServerPort { get; set; }
        public bool UseSsl { get; set; }

        public string SenderName { get; set; }
        public string SenderAdress { get; set; }
        public string SenderPassword { get; set; }
    }
}
