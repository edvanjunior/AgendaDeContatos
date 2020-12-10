using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication.Repository;
using WebApplication.ViewModels;

namespace WebApplication.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleController(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        // GET: api/People
        [HttpGet]
        public async Task<IEnumerable<Person>> GetPeople()
        {
            return await _peopleRepository.GetPeopleAsync();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _peopleRepository.GetPeopleByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, EditPersonViewModel person)
        {
            if (id != person.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            

            try
            {
                Person p = new Person
                {
                    Nome = person.Nome
                };
                await _peopleRepository.UpdatePersonAsync(p);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/People
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(PersonViewModel person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Person p = new Person
            {
                Nome = person.Nome
            };

            await _peopleRepository.CreatePersonAsync(p);
            return StatusCode(201);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            var person = await _peopleRepository.GetPeopleByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            await _peopleRepository.DeletePersonAsync(id);

            return person;
        }

        private bool PersonExists(int id)
        {
            return _peopleRepository.PersonExists(id);
        }
    }
}
