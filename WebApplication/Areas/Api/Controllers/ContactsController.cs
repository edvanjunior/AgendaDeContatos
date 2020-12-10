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
    public class ContactsController : ControllerBase
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IPeopleRepository _peopleRepository;

        public ContactsController(IContactsRepository contactsRepository, IPeopleRepository peopleRepository)
        {
            _contactsRepository = contactsRepository;
            _peopleRepository = peopleRepository;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<IEnumerable<Contact>> GetContacts()
        {
            return await _contactsRepository.GetContactsAsync();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _contactsRepository.GetContactsByIdAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, EditContactViewModel contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            //_context.Entry(contact).State = EntityState.Modified;

            try
            {
                Contact c = new Contact
                {
                    Id = contact.Id,
                    PersonId = contact.PersonId,
                    Type = contact.Type,
                    Value = contact.Value
                };
                await _contactsRepository.UpdateContactsAsync(c);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
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

        // POST: api/Contacts
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(CreateContactViewModel contact)
        {
            Contact c = new Contact
            {
                PersonId = contact.PersonId,
                Type = contact.Type,
                Value = contact.Value
            };
            await _contactsRepository.CreateContactAsync(c);

            return StatusCode(201);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> DeleteContact(int id)
        {
            var contact = await _contactsRepository.GetContactsByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            await _contactsRepository.DeleteContactAsync(id);

            return contact;
        }

        private bool ContactExists(int id)
        {
            return _contactsRepository.ContactExists(id);
        }
    }
}
