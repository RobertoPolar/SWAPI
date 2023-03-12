using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel;
using WebApplication1.Controllers;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Data.ViewModels;

namespace WebApplication1.Services
{
    public class PeopleService
    {
        private readonly GettrxContext _context;
        private readonly ILogger<PeopleService> _logger;

        public PeopleService(GettrxContext context, ILogger<PeopleService> logger)
        {
            _context = context;
            _logger = logger;   
        }

        public async Task<IEnumerable<Person>> GetPeopleFromDB()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<List<PersonViewModel>> GetPeopleFromAPI()
        {
            PeopleViewModel peopleResponse = new PeopleViewModel();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://swapi.dev/api/people/"))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            peopleResponse = JsonConvert.DeserializeObject<PeopleViewModel>(apiResponse);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(message: $"Error getting GetPeopleFromAPI. Error: {e.Message}");
            }


            return peopleResponse.results;
        }

        public async Task<PersonViewModel> GetAnyPersonById(int id)
        {
            PersonViewModel person = new PersonViewModel();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://swapi.dev/api/people/" + id))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            person = JsonConvert.DeserializeObject<PersonViewModel>(apiResponse);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error getting GetAnyPersonById from API. Error: {e.Message}");
            }

            return person;

        }

        public async Task<Person?> GetPeopleById(int id)
        {
            return await _context.People.FindAsync(id);
        }

        public async Task<Person> CreatePeople(Person person)
        {
            try
            {
                _context.People.Add(person);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(message: $"Error creating People. Error: {e.Message}");
            }
            return person;
        }

        public async Task UpdatePeople(Person person)
        {
            try
            {
                var existingPeople = await GetPeopleById(person.Id);

                if (existingPeople is not null)
                {
                    existingPeople.Name = person.Name;
                    existingPeople.BirthYear = person.BirthYear;
                    existingPeople.EyeColor = person.EyeColor;
                    existingPeople.Gender = person.Gender;
                    existingPeople.HairColor = person.HairColor;
                    existingPeople.Height = person.Height;
                    existingPeople.Homeworld = person.Homeworld;
                    existingPeople.Mass = person.Mass;
                    existingPeople.SkinColor = person.SkinColor;
                    existingPeople.Created = person.Created;
                    existingPeople.Edited = person.Edited;
                    existingPeople.Url = person.Url;

                    _context.People.Update(existingPeople);

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(message: $"Error updating People. Error: {e.Message}");
            }
        }

        public async Task DeletePeople(Person person)
        {
            try
            {
                var existingPeople = await GetPeopleById(person.Id);

                if (existingPeople is not null)
                {
                    _context.People.Remove(existingPeople);

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(message: $"Error deleting People. Error: {e.Message}");
            }
        }
    }
}
