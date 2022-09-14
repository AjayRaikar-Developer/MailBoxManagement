using System;
using System.Collections.Generic;
using System.Text;

namespace MailBoxManagement.DTOs
{
    public class MailBoxFetchResponseDTO : GenericResponseDTO
    {
        public MailBoxFetchResponseDTO()
        {
            MailBoxDetails = new List<MailBoxDTO>();
        }
        public List<MailBoxDTO> MailBoxDetails { get; set; }
    }
}
