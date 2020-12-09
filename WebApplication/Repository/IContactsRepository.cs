using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public interface IContactsRepository
    {
        Task<IEnumerable<Contact>> GetContactsAsync();

        Task<Contact> GetContactsByIdAsync(int? id);

        Task UpdateContactsAsync(Contact contact);

        Task CreateContactAsync(Contact contact);

        Task DeleteContactAsync(int? id);

        bool ContactExists(int? id);
    }
}
