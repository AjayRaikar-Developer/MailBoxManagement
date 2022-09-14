using MailBoxManagement.DTOs;
using MailManagement.Interface;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailBoxManagement.Outlook
{
    public class OutlookManagement : IMailBoxManagement
    {
        private ExchangeService _service;
        // This is the office365 webservice URL
        private const string _mailProvideUrl = "https://outlook.office365.com/EWS/Exchange.asmx";

        public OutlookManagement(string mailBoxEmailAddress, string mailBoxPassword)
        {
            _service = new ExchangeService
            {
                Credentials = new WebCredentials(mailBoxEmailAddress, mailBoxPassword)
            };
            _service.Url = new Uri(_mailProvideUrl);
        }

        public MailBoxFetchResponseDTO FetchAllMailBoxMessages()
        {
            MailBoxFetchResponseDTO response = new MailBoxFetchResponseDTO();
            try
            {
                FindItemsResults<Item> findResults = _service.FindItems(WellKnownFolderName.Inbox, new ItemView(int.MaxValue));
                response.MailBoxDetails = LoadMailContents(findResults);
                response.ResponseMessage = "Data Successfully Fetched";
                response.Status = BusinessStatus.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseMessage = "Some Exception has occurred - " + ex.ToString();
                response.Status = BusinessStatus.Error;
                return response;
            }
        }

        public MailBoxFetchResponseDTO FetchAllUnReadMailBoxMessages(int numberOfRecords)
        {
            MailBoxFetchResponseDTO response = new MailBoxFetchResponseDTO();
            try
            {
                SearchFilter searchfltr = new SearchFilter.SearchFilterCollection(LogicalOperator.And,
                                                           new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false));

                ItemView itemView = new ItemView((numberOfRecords <= 0) ? 10 : numberOfRecords);
                FindItemsResults<Item> findResults = _service.FindItems(WellKnownFolderName.Inbox, searchfltr, itemView);

                response.MailBoxDetails = LoadMailContents(findResults);
                response.ResponseMessage = "All UnRead Email Data Successfully Fetched";
                response.Status = BusinessStatus.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseMessage = "Some Exception has occurred - " + ex.ToString();
                response.Status = BusinessStatus.Error;
                return response;
            }
        }

        public MailBoxFetchResponseDTO SearchMailBoxMessages(MailBoxSearchDTO searchDto)
        {
            MailBoxFetchResponseDTO response = new MailBoxFetchResponseDTO();
            try
            {
                #region Filter Samples


                /* Search based on date greater than or equal to */
                //SearchFilter searchfltdate = new SearchFilter.IsGreaterThanOrEqualTo(ItemSchema.DateTimeCreated, dateTime);

                /* Search for the matching string against the subject filed in Mailbox */
                //SearchFilter searchfltr = new SearchFilter.SearchFilterCollection(LogicalOperator.And,
                // new SearchFilter.ContainsSubstring(EmailMessageSchema.Subject, "s"));

                #endregion

                var searchfltr = new SearchFilter.SearchFilterCollection();
                searchfltr.AddRange(SearchFilterObjCreation(searchDto));
                searchfltr.LogicalOperator = LogicalOperator.Or;

                FindItemsResults<Item> findResults = _service.FindItems(WellKnownFolderName.Inbox, searchfltr,
                                                                        new ItemView(int.MaxValue));
                response.MailBoxDetails = LoadMailContents(findResults);
                response.ResponseMessage = "Data Successfully Fetched";
                response.Status = BusinessStatus.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseMessage = "Some Exception has occurred - " + ex.ToString();
                response.Status = BusinessStatus.Error;
                return response;
            }
        }

        #region Private Methods
        private List<MailBoxDTO> LoadMailContents(FindItemsResults<Item> mailItems)
        {
            List<MailBoxDTO> result = new List<MailBoxDTO>();

            if (mailItems.Items.Count <= 0)
            {
                return result;
            }

            PropertySet PropSet = new PropertySet();
            PropSet.Add(ItemSchema.DisplayTo);
            PropSet.Add(ItemSchema.DisplayCc);
            PropSet.Add(ItemSchema.Subject);
            PropSet.Add(ItemSchema.Body);
            PropSet.Add(ItemSchema.TextBody);
            PropSet.Add(ItemSchema.HasAttachments);
            PropSet.Add(ItemSchema.Attachments);
            PropSet.Add(ItemSchema.DateTimeCreated);
            PropSet.Add(ItemSchema.DateTimeReceived);

            _service.LoadPropertiesForItems(mailItems.Items, PropSet);

            foreach (var item in mailItems.Items)
            {

                EmailMessage message = EmailMessage.Bind(_service, item.Id);

                MailBoxDTO mail = new MailBoxDTO();
                mail.Id = item.Id;
                mail.DisplayFrom = message.From;
                mail.DisplayTo = message.ToRecipients;
                mail.DisplayCc = message.CcRecipients;
                mail.DisplayBcc = message.BccRecipients;
                mail.Subject = item.Subject;
                mail.Body = item.Body.Text;
                mail.TextBody = item.TextBody;
                mail.HasAttachments = item.HasAttachments;
                mail.Attachments = item.Attachments;
                mail.DateTimeCreated = item.DateTimeCreated;
                mail.DateTimeReceived = item.DateTimeReceived;
                result.Add(mail);

                //Change the status of the mail to read
                message.IsRead = true;
                message.Update(ConflictResolutionMode.AutoResolve);

            }

            return result;
        }
        private SearchFilter[] SearchFilterObjCreation(MailBoxSearchDTO searchDTO)
        {
            List<SearchFilter> response = new List<SearchFilter>();

            if (!String.IsNullOrEmpty(searchDTO.FromEmail))
            {
                response.Add(new SearchFilter.ContainsSubstring(EmailMessageSchema.From, searchDTO.FromEmail));
            }

            if (!String.IsNullOrEmpty(searchDTO.ToEmail))
            {
                response.Add(new SearchFilter.ContainsSubstring(EmailMessageSchema.ToRecipients, searchDTO.ToEmail));
            }

            if (!String.IsNullOrEmpty(searchDTO.Body))
            {
                response.Add(new SearchFilter.ContainsSubstring(EmailMessageSchema.Body, searchDTO.Body));
            }

            if (!String.IsNullOrEmpty(searchDTO.Subject))
            {
                response.Add(new SearchFilter.ContainsSubstring(EmailMessageSchema.Subject, searchDTO.Subject));
            }

            if (!String.IsNullOrEmpty(searchDTO.FromDateTime))
            {
                response.Add(new SearchFilter.IsGreaterThanOrEqualTo(EmailMessageSchema.DateTimeCreated, searchDTO.FromDateTime));
            }

            if (!String.IsNullOrEmpty(searchDTO.ToDateTime))
            {
                response.Add(new SearchFilter.IsLessThanOrEqualTo(ItemSchema.DateTimeReceived, searchDTO.ToDateTime));
            }

            return response.ToArray();
        }
        #endregion
    }
}
