using MailBoxManagement.DTOs;

namespace MailBoxManagement.Interface
{
    public interface IMailBoxManagement
    {
        MailBoxFetchResponseDTO FetchAllMailBoxMessages();
        MailBoxFetchResponseDTO FetchAllUnReadMailBoxMessages(int numberOfRecords);
        MailBoxFetchResponseDTO SearchMailBoxMessages(MailBoxSearchDTO searchDto);
    }
}
