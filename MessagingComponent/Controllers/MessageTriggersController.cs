using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealmDigital.Logic.Interfaces;
using System.Threading.Tasks;

namespace RealmDigital.MessagingService.Api.Controllers
{
    [Route("api/triggers")]
    [ApiController]
    public class MessageTriggersController : ControllerBase
    {
        private readonly IEmployeeMessagesLogic _logic;
        private readonly ILogger<MessageTriggersController> _logger;

        public MessageTriggersController(IEmployeeMessagesLogic logic, ILogger<MessageTriggersController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        [HttpGet("messages")]
        public async Task<ActionResult> TriggerBirthdayMessage()
        {
            _logger.LogInformation("Trigger: Sending birthday messages");

            await _logic.SendBirthdayMessagesAsync();

            _logger.LogInformation("Done sending birthday messages.");

            return Ok();
        }
    }
}
