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
    public class AddressesController : ControllerBase
    {
        private readonly IAddressesRepository _addressesRepository;
        private readonly IPeopleRepository _peopleRepository;

        public AddressesController(IAddressesRepository addressesRepository, IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
            _addressesRepository = addressesRepository;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<IEnumerable<Address>> GetAddresses()
        {
            return await _addressesRepository.GetAddressesAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _addressesRepository.GetAddressesByIdAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, EditAddressViewModel address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            

            try
            {
                Address a = new Address
                {
                    Id = address.Id,
                    AddressType = address.AddressType,
                    City = address.City,
                    Complement = address.Complement,
                    Location = address.Location,
                    Neighborhood = address.Neighborhood,
                    Number = address.Number,
                    PersonId = address.PersonId,
                    State = address.State
                };
                await _addressesRepository.UpdateAddressesAsync(a);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Address a = new Address
            {
                AddressType = address.AddressType,
                City = address.City,
                Complement = address.Complement,
                Location = address.Location,
                Neighborhood = address.Neighborhood,
                Number = address.Number,
                PersonId = address.PersonId,
                State = address.State
            };
            await _addressesRepository.CreateAddressAsync(a);

            return StatusCode(201);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            var address = await _addressesRepository.GetAddressesByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            await _addressesRepository.DeleteAddressAsync(id);

            return address;
        }

        private bool AddressExists(int id)
        {
            return _addressesRepository.AddressExists(id);
        }
    }
}
