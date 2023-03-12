using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using WebApplication1.Controllers;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Data.ViewModels;
using WebApplication1.Services;

namespace TestProject1
{
    public class PeopleTest
    {
        private readonly PeopleController _controller;
        private readonly PeopleService _service;
        private readonly ILogger<PeopleService> _loggerService;
        private readonly GettrxContext _context;

        private IConfigurationRoot _configuration;

        private DbContextOptions<GettrxContext> _options;

        public PeopleTest()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<GettrxContext>()
                .UseSqlServer(_configuration.GetConnectionString("DBConnection"))
                .Options;

            _loggerService = new NullLogger<PeopleService>();
            _context = new GettrxContext(_options);
            _service = new PeopleService(_context, _loggerService);
            _controller = new PeopleController(_service);
        }

        [Fact]
        public async Task GetPeopleWithLimit_Ok()
        {
            int id = 1;

            var result = await _controller.GetPeopleWithLimit(id);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetPeopleById_Ok()
        {
            int id = 99;

            var result = await _controller.GetPeopleById(id);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetPeopleFromDB_Ok()
        {
            var result = _service.GetPeopleFromDB();

            Assert.IsAssignableFrom<IEnumerable<Person>>(result);
        }

        [Fact]
        public async Task GetPeopleFromAPI_Ok()
        {
            var result = await _service.GetPeopleFromAPI();

            Assert.IsAssignableFrom<List<PersonViewModel>>(result);
        }

        [Fact]
        public async Task GetAnyPersonById_Ok()
        {
            int id = 1;
            var result = await _service.GetAnyPersonById(id);

            Assert.IsAssignableFrom<PersonViewModel>(result);
        }

        [Fact]
        public async Task GetPeopleByIdService_Ok()
        {
            int id = 1;
            var result = await _service.GetPeopleById(id);

            Assert.IsAssignableFrom<Person>(result);
        }

    }
}