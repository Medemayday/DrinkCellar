using DrinkCellar.Api.DTOs.CellarDTO;
using DrinkCellar.Api.Extensions;
using DrinkCellar.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrinkCellar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CellarController(ICellarService cellarService) : ControllerBase
    {
        private readonly ICellarService _cellarService = cellarService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _cellarService.GetAllAsync();
            if (!result.IsSucces)
            {
                return BadRequest(result.ValidationErrors);

            }

            return Ok(result.Items.ToResponseDTOS());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _cellarService.GetByIdAsync(id);
            if (!result.IsSucces)
            {
                return BadRequest(result.ValidationErrors);
            }

            return Ok(result.Items.FirstOrDefault().ToResponseDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CellarRequestDTO cellarRequestDTO)
        {
            var result = await _cellarService.AddAsync
                (cellarRequestDTO.Name, cellarRequestDTO.MaxCapacity, cellarRequestDTO.Cooled);

            if (!result.IsSucces)
            {
                return BadRequest(result.ValidationErrors);
            }

            return Ok();
        }
    }
}
