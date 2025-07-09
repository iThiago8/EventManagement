using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using apis.Models;

namespace apis.Data
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ScientificCommittee> ScientificCommittees { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Symposium> Symposia { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
    }
}
