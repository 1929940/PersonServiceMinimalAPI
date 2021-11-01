using Microsoft.EntityFrameworkCore;
using PersonServiceMinimalAPI.Models;

namespace PersonServiceMinimalAPI.Context {
    public class Context : DbContext {
        public Context(DbContextOptions options) : base(options) {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
