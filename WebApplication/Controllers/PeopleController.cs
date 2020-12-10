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
    public class PeopleController : Controller
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleController(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(await _peopleRepository.GetPeopleAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _peopleRepository.GetPeopleByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] PersonViewModel person)
        {
            if (ModelState.IsValid)
            {
                Person p = new Person
                {
                    Nome = person.Nome
                };
                await _peopleRepository.CreatePersonAsync(p);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _peopleRepository.GetPeopleByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] EditPersonViewModel person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Person p = new Person
                    {
                        Id = person.Id,
                        Nome = person.Nome
                    };
                    await _peopleRepository.UpdatePersonAsync(p);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _peopleRepository.GetPeopleByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _peopleRepository.DeletePersonAsync(id);
            
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _peopleRepository.PersonExists(id);
        }
    }
}
