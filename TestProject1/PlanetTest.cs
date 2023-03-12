using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Data.ViewModels;
using WebApplication1.Services;

namespace TestProject1
{
    public class PlanetTest
    {
        private readonly PlanetController _controller;
        private readonly PlanetService _service;
        private readonly ILogger<PlanetService> _loggerService;
        private readonly GettrxContext _context;

        private IConfigurationRoot _configuration;

        private DbContextOptions<GettrxContext> _options;

        public PlanetTest()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<GettrxContext>()
                .UseSqlServer(_configuration.GetConnectionString("DBConnection"))
                .Options;

            _loggerService= new NullLogger<PlanetService>();
            _context = new GettrxContext(_options);
            _service = new PlanetService(_context, _loggerService);
            _controller = new PlanetController(_service);
        }

        [Fact]
        public async Task GetPeopleWithLimit_Ok()
        {
            int id = 1;

            var result = await _controller.GetPlanetWithLimit(id);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetPeopleById_Ok()
        {
            int id = 2;

            var result = await _controller.GetPlanetById(id);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetPeopleFromDB_Ok()
        {
            var result = _service.GetPeopleFromDB();

            Assert.IsAssignableFrom<IEnumerable<Planet>>(result);

        }

        [Fact]
        public async Task GetPlanetFromAPI_Ok()
        {
            var result = await _service.GetPlanetFromAPI();

            Assert.IsAssignableFrom<List<PlanetViewModel>>(result);

        }

        [Fact]
        public async Task GetAnyPlanetById_Ok()
        {
            int id = 1;

            var result = await _service.GetAnyPlanetById(id);

            Assert.IsAssignableFrom<PlanetViewModel>(result);

        }

        [Fact]
        public async Task GetPlanetById_Ok()
        {
            int id = 99;

            var result = await _service.GetPlanetById(id);

            Assert.IsAssignableFrom<Planet>(result);

        }
    }
}
