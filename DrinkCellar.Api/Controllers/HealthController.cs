using Microsoft.AspNetCore.Mvc;

namespace DrinkCellar.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public string Alive()
        {
            return "Yes, I'm Alive";
        }
    }
}
