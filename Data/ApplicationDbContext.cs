using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using apis.Models;

namespace apis.Data
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<ScientificCommittee> ScientificCommittee { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Symposium> Symposium { get; set; }
        public DbSet<Workshop> Workshop { get; set; }
    }
}
