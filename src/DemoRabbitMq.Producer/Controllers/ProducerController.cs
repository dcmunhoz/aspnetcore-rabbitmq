using DemoRabbitMq.Producer.Infrastructure.Bus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace DemoRabbitMq.Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {

        private readonly IBus _bus;


        public ProducerController(IBus bus)
        {
            _bus = bus;
        }


        [HttpPost]
        public async Task<IActionResult> Prducer([FromBody] string message)
        {
            await _bus.PublicAsync(message);

            return Ok(message);
        }
    }
}
