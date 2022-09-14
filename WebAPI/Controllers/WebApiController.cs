using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MailBoxManagement.Outlook;
using MailBoxManagement.DTOs;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using System.Text;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WebApiController : ControllerBase
    {
        private readonly ILogger<WebApiController> _logger;
        private IConfiguration _configuration;
        private string _emailAddress;
        private string _password;

        public WebApiController(ILogger<WebApiController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _emailAddress = _configuration["Out_Look:EmailAddress"];
            _password = _configuration["Out_Look:Password"];
        }

        [HttpGet]
        public dynamic FetchAllUnReadMailBoxMessages()
        {
            var mailBox = new OutlookManagement(_emailAddress, _password);
            return mailBox.FetchAllUnReadMailBoxMessages(100);
        }

        [HttpGet]
        public dynamic FetchAllMailBoxMessages()
        {
            var mailBox = new OutlookManagement(_emailAddress, _password);
            return mailBox.FetchAllMailBoxMessages();
        }

        [HttpGet]
        public dynamic SearchMailBoxMessages(MailBoxSearchDTO searchDTO)
        {
            var mailBox = new OutlookManagement(_emailAddress, _password);
            return mailBox.SearchMailBoxMessages(searchDTO);
        }
    }
}
