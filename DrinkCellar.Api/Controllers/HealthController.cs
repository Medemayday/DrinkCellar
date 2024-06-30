using Microsoft.AspNetCore.Mvc;

namespace DrinkCellar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public string Health()
        {
            return "Yes, I'm Alive";
        }
    }
}
