using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailBoxManagement.DTOs
{
    public class MailBoxSearchDTO
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromDateTime { get; set; }
        public string ToDateTime { get; set; }
    }
}
