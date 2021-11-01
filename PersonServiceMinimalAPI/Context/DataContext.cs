using Microsoft.EntityFrameworkCore;
using PersonServiceMinimalAPI.Models;

namespace PersonServiceMinimalAPI.Context {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions options) : base(options) {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
