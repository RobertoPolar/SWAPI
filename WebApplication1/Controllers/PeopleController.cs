using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Data.ViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PeopleController : Controller
    {
        private readonly PeopleService _service;

        public PeopleController(PeopleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPeopleWithLimit([FromQuery(Name = "limit")] int limit)
        {
            var result = await _service.GetPeopleFromAPI();

            return Ok(result.Take(limit));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPeopleById(int id)
        {
            if (id <= 0) return BadRequest(new { message = "ID must be greater than 0" });

            Person? person = await _service.GetPeopleById(id);
            if (person is not null) return Ok(person);

            PersonViewModel personView = new PersonViewModel();
            personView = await _service.GetAnyPersonById(id);

            if (personView is null || person is null) {
                return NotFound();
            }

            return Ok(personView);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Person request)
        {
            if (request.Id <= 0) return BadRequest(new { message = "ID must be greater than 0" });

            Person? person = await _service.GetPeopleById(request.Id);
            if (person is null) return NotFound();

            var newPerson = await _service.CreatePeople(request);

            return CreatedAtAction(nameof(GetPeopleById),new { id = newPerson.Id, newPerson});
        }
    }
}
