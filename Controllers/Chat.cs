using Microsoft.AspNetCore.Mvc;

namespace Elipse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ChatController> _logger;

        public ChatController(ILogger<ChatController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetChat")]
        public IEnumerable<Chat> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Chat
            {
                Id = Random.Shared.Next(0, 55),
                Text = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
