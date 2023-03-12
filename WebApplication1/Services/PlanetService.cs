using Newtonsoft.Json;
using WebApplication1.Controllers;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Data.ViewModels;

namespace WebApplication1.Services
{
    public class PlanetService
    {
        private readonly GettrxContext _context;
        private readonly ILogger<PlanetService> _logger;

        public PlanetService(GettrxContext context, ILogger<PlanetService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Planet> GetPeopleFromDB()
        {
            return _context.Planets.ToList();
        }

        public async Task<List<PlanetViewModel>> GetPlanetFromAPI()
        {
            PlanetsViewModel planetResponse = new PlanetsViewModel();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://swapi.dev/api/planets/"))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            planetResponse = JsonConvert.DeserializeObject<PlanetsViewModel>(apiResponse);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error getting Planet from API. Error: {e.Message}");
            }

            return planetResponse.results;
        }

        public async Task<PlanetViewModel> GetAnyPlanetById(int id)
        {
            PlanetViewModel planet = new PlanetViewModel();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://swapi.dev/api/planets/" + id))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            planet = JsonConvert.DeserializeObject<PlanetViewModel>(apiResponse);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error getting AnyPlanet from API. Error: {e.Message}");
            }
            

            return planet;

        }

        public async Task<Planet?> GetPlanetById(int id)
        {
            return await _context.Planets.FindAsync(id);
        }
    }
}
