using Library.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dal.Concrete.Context
{
    public class LibraryDbContext : DbContext 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(local);Database=LibraryDb;Integrated Security=True;TrustServerCertificate=True");
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}
