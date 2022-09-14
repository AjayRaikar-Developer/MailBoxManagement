using System;
using System.Collections.Generic;
using System.Text;

namespace MailBoxManagement.DTOs
{
    public class MailBoxDTO
    {
        public dynamic Id { get; set; }
        public dynamic DisplayFrom { get; set; }
        public dynamic DisplayTo { get; set; }
        public dynamic DisplayCc { get; set; }
        public dynamic DisplayBcc { get; set; }
        public dynamic Subject { get; set; }
        public dynamic TextBody { get; set; }
        public dynamic Attachments { get; set; }
        public dynamic HasAttachments { get; set; }
        public dynamic Body { get; set; }
        public dynamic DateTimeCreated { get; set; }
        public dynamic DateTimeReceived { get; set; }
    }
}
