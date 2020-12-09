using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly Context _context;
        public ContactsRepository( Context context)
        {
            _context = context;
        }

        public bool ContactExists(int? id)
        {
            return  _context.Contacts.Any(c => c.Id == id);
        }

        public async Task CreateContactAsync(Contact contact)
        {
            _context.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(int? id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return await _context.Contacts.Include(c => c.Person).ToListAsync();
        }

        public async Task<Contact> GetContactsByIdAsync(int? id)
        {
           return await _context.Contacts
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateContactsAsync(Contact contact)
        {
            _context.Update(contact);
            await _context.SaveChangesAsync();
        }
    }
}
