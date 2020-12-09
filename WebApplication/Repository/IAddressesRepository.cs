using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public interface IAddressesRepository
    {
        Task<IEnumerable<Address>> GetAddressesAsync();

        Task<Address> GetAddressesByIdAsync(int? id);

        Task UpdateAddressesAsync(Address address);

        Task CreateAddressAsync(Address address);

        Task DeleteAddressAsync(int? id);

        bool AddressExists(int? id);
    }
}
