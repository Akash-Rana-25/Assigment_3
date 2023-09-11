using Assingment_3.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Assingment_3.Contex
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
