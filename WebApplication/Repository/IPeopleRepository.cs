using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public interface IPeopleRepository
    {
        Task<IEnumerable<Person>> GetPeopleAsync();

        Task<Person> GetPeopleByIdAsync(int? id);

        Task UpdatePersonAsync(Person person);

        Task CreatePersonAsync(Person person);

        Task DeletePersonAsync(int? id);

        bool PersonExists(int? id);
    }
}
