using MailBoxManagement.Outlook;
using MailBoxManagement.Interface;

namespace MailBoxManagement.Service
{
    public class MailBoxManagementService
    {
        private string _mailBoxEmailAddress;
        private string _mailBoxPassword;
        public MailBoxManagementService(string mailBoxEmailAddress, string mailBoxPassword)
        {
            _mailBoxEmailAddress = mailBoxEmailAddress;
            _mailBoxPassword = mailBoxPassword;
        }

        public IMailBoxManagement ChooseProvider(string providerName)
        {
            switch (providerName)
            {
                case "Outlook": 
                    return new OutlookManagement(_mailBoxEmailAddress, _mailBoxPassword);
                default: 
                    return new OutlookManagement(_mailBoxEmailAddress, _mailBoxPassword);
            }

        }
    }
}
