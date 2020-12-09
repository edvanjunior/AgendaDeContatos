using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Context:DbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Address> Addresses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Server=JUNIOR\SQLEXPRESS;Database=AgendaDeContatos;Integrated Security =True");
        }
    }
}
