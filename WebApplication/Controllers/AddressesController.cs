using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication.Repository;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class AddressesController : Controller
    {
        private readonly IAddressesRepository _addressesRepository;
        private readonly IPeopleRepository _peopleRepository;

        public AddressesController(IAddressesRepository addressesRepository, IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
            _addressesRepository = addressesRepository;
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            return View(await _addressesRepository.GetAddressesAsync());
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _addressesRepository.GetAddressesByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Addresses/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PersonId"] = new SelectList(await _peopleRepository.GetPeopleAsync(), "Id", "Nome");
            return View();
        }

        // POST: Addresses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Location,Number,Complement,AddressType,Neighborhood,City,State,PersonId")] CreateAddressViewModel address)
        {
            if (ModelState.IsValid)
            {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(await _peopleRepository.GetPeopleAsync(), "Id", "Nome", address.PersonId);
            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _addressesRepository.GetAddressesByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(await _peopleRepository.GetPeopleAsync(), "Id", "Nome", address.PersonId);
            return View(address);
        }

        // POST: Addresses/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Location,Number,Complement,AddressType,Neighborhood,City,State,PersonId")] EditAddressViewModel address)
        {
            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                    await _addressesRepository.UpdateAddressesAsync(a);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(await _peopleRepository.GetPeopleAsync(), "Id", "Nome", address.PersonId);
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _addressesRepository.GetAddressesByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _addressesRepository.DeleteAddressAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _addressesRepository.AddressExists(id);
        }
    }
}
