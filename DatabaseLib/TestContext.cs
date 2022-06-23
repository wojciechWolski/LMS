using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLib
{
    public class TestContext : DbContext
    {
        
        public DbSet<Admin> Admins { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"DataSource=localhost; Database=library; Trusted_Connection=True");
        }
    }
}
