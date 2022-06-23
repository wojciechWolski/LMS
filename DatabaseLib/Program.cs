using Microsoft.EntityFrameworkCore;

namespace DatabaseLib
{
    public class Lib
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LibDbContext : DbContext
    {
        public DbSet<Lib> Libs { get; set; }
    }
}