using System;
using System.Collections.Generic;
using System.Text;

namespace MailBoxManagement.DTOs
{
    public class GenericResponseDTO
    {
        public string ResponseMessage { get; set; }
        public string Id { get; set; }
        public BusinessStatus Status { get; set; }
        public List<ErrorInfo> Errors { get; set; }
        public dynamic Response { get; set; }
    }

    public class ErrorInfo
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
    }

    public enum BusinessStatus
    {
        None = 0,
        Ok = 1,
        Created = 2,
        Updated = 3,
        NotFound = 4,
        Deleted = 5,
        NothingModified = 6,
        Error = 7,
        PreConditionFailed = 8,
        InputValidationFailed = 9,
        NotImplemented = 10,
        UnAuthorized = 11,
        ServiceUnAvailable = 12
    }
}
