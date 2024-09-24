using DrinkCellar.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DrinkCellar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CellarController(ICellarService cellarService) : ControllerBase
    {
        private readonly ICellarService _cellarService = cellarService;




    }
}
