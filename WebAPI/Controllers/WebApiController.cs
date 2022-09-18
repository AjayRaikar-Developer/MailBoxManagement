using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MailBoxManagement.DTOs;
using MailBoxManagement.Interface;
using MailBoxManagement.Service;
using MailBoxManagement.Outlook;

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
        private IMailBoxManagement _mailService;

        public WebApiController(ILogger<WebApiController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _emailAddress = _configuration["Out_Look:EmailAddress"];
            _password = _configuration["Out_Look:Password"];
            _mailService = new MailBoxManagementService(_emailAddress, _password).ChooseProvider("Outlook");
        }

        [HttpGet]
        public dynamic FetchAllUnReadMailBoxMessages()
        {
            return _mailService.FetchAllUnReadMailBoxMessages(100);
        }

        [HttpGet]
        public dynamic FetchAllMailBoxMessages()
        {
            return _mailService.FetchAllMailBoxMessages();
        }

        [HttpGet]
        public dynamic SearchMailBoxMessages(MailBoxSearchDTO searchDTO)
        {
            return _mailService.SearchMailBoxMessages(searchDTO);
        }
    }
}
