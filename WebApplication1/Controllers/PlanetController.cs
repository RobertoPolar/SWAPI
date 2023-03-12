using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Models;
using WebApplication1.Data.ViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PlanetController : Controller
    {
        private readonly PlanetService _service;

        public PlanetController(PlanetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlanetWithLimit([FromQuery(Name = "limit")] int limit)
        {
            var result = await _service.GetPlanetFromAPI();

            return Ok(result.Take(limit));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlanetById(int id)
        {
            if (id <= 0) return BadRequest(new { message = "ID must be greater than 0" });

            Planet? person = await _service.GetPlanetById(id);
            PlanetViewModel planetView = new PlanetViewModel();

            if (person is not null) return Ok(person);

            planetView = await _service.GetAnyPlanetById(id);

            if (planetView is null) return NotFound();

            return Ok(planetView);
        }
    }
}
