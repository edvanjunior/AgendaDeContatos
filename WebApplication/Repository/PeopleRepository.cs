using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly Context _context;

        public PeopleRepository(Context context)
        {
            _context = context;
        }

        public async Task CreatePersonAsync(Person person)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(int? id)
        {
            var person = await _context.People.FindAsync(id);
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
           return await _context.People.ToListAsync();
        }

        public async Task<Person> GetPeopleByIdAsync(int? id)
        {
            return await _context.People.Include(c => c.Contacts).Include(a => a.Addresses)
                 .FirstOrDefaultAsync(m => m.Id == id);
        }

        public  bool PersonExists(int? id) =>   _context.People.Any(e => e.Id == id);

        public async Task UpdatePersonAsync(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            _context.Update(person);
            await _context.SaveChangesAsync();
        }

    }
}
