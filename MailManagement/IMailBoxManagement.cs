using MailBoxManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailManagement.Interface
{
    public interface IMailBoxManagement
    {
        MailBoxFetchResponseDTO FetchAllMailBoxMessages();
        MailBoxFetchResponseDTO FetchAllUnReadMailBoxMessages(int numberOfRecords);
        MailBoxFetchResponseDTO SearchMailBoxMessages(MailBoxSearchDTO searchDto);
    }
}
