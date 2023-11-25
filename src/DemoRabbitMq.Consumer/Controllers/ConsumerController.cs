using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoRabbitMq.Consumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly IMessageService _service;

        public ConsumerController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetMessages()
        {
            return Ok(_service.GetMessages());
        }
    }
}
