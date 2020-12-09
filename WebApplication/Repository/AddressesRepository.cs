using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class AddressesRepository : IAddressesRepository
    {
        private readonly Context _context;
        public AddressesRepository(Context context)
        {
            _context = context;
        }

        public bool AddressExists(int? id)
        {
            return _context.Addresses.Any(c => c.Id == id);
        }

        public async Task CreateAddressAsync(Address address)
        {
            _context.Add(address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(int? id)
        {
            var address = await _context.Addresses.FindAsync(id);
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesAsync()
        {
            return await _context.Addresses.Include(c => c.Person).ToListAsync();
        }

        public async Task<Address> GetAddressesByIdAsync(int? id)
        {
            return await _context.Addresses
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAddressesAsync(Address address)
        {
            _context.Update(address);
            await _context.SaveChangesAsync();
        }
    }
}
