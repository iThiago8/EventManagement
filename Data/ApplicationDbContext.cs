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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleReview>()
                .HasKey(asc => new { asc.ArticleId, asc.ScientificCommitteeId });


            modelBuilder.Entity<ArticleReview>()
                .HasOne(asc => asc.Article)
                .WithMany(a => a.ArticleReviews)
                .HasForeignKey(asc => asc.ArticleId);

            modelBuilder.Entity<ArticleReview>()
                .HasOne(asc => asc.ScientificCommittee)
                .WithMany(sc => sc.ArticleReviews)
                .HasForeignKey(asc => asc.ScientificCommitteeId);
        }
    }
}
