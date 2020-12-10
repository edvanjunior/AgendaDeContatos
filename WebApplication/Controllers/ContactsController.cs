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
    public class ContactsController : Controller
    {
        private readonly IContactsRepository  _contactsRepository;
        private readonly IPeopleRepository _peopleRepository;

        public ContactsController(IContactsRepository contactsRepository, IPeopleRepository peopleRepository)
        {
            _contactsRepository = contactsRepository;
            _peopleRepository = peopleRepository;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            return View(await _contactsRepository.GetContactsAsync());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _contactsRepository.GetContactsByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PersonId"] = new SelectList(await _peopleRepository.GetPeopleAsync(), "Id", "Nome");
            return View();
        }

        // POST: Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,Value,PersonId")] CreateContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                Contact c = new Contact
                {
                    PersonId = contact.PersonId,
                    Type = contact.Type,
                    Value = contact.Value
                };
                await _contactsRepository.CreateContactAsync(c);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(await _peopleRepository.GetPeopleAsync(), "Id", "Nome", contact.PersonId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _contactsRepository.GetContactsByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(await _peopleRepository.GetPeopleAsync(), "Id", "Nome", contact.PersonId);
            return View(contact);
        }

        // POST: Contacts/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Value,PersonId")] EditContactViewModel contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Contact c = new Contact
                    {
                        PersonId = contact.PersonId,
                        Type = contact.Type,
                        Value = contact.Value
                    };
                    await _contactsRepository.UpdateContactsAsync(c);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
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
            ViewData["PersonId"] = new SelectList(await _peopleRepository.GetPeopleAsync(), "Id", "Nome", contact.PersonId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _contactsRepository.GetContactsByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _contactsRepository.DeleteContactAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _contactsRepository.ContactExists(id);
        }
    }
}
