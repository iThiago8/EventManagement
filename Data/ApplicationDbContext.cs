using apis.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace apis.Data
{
    public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<ScientificCommittee> ScientificCommittee { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Symposium> Symposium { get; set; }
        public DbSet<Workshop> Workshop { get; set; }
        public DbSet<WorkshopSymposium> WorkshopSymposium { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });

            modelBuilder.Entity<ArticleReview>()
                .HasKey(asc => new { asc.ArticleId, asc.ScientificCommitteeId });

            modelBuilder.Entity<ArticleReview>()
                .HasOne(asc => asc.Article)
                .WithMany(a => a.ArticleReview)
                .HasForeignKey(asc => asc.ArticleId);

            modelBuilder.Entity<ArticleReview>()
                .HasOne(asc => asc.ScientificCommittee)
                .WithMany(sc => sc.ArticleReview)
                .HasForeignKey(asc => asc.ScientificCommitteeId);

            modelBuilder.Entity<PersonSymposium>()
                .HasKey(ps => new { ps.PersonId, ps.SymposiumId });

            modelBuilder.Entity<PersonSymposium>()
                .HasOne(ps => ps.Person)
                .WithMany(p => p.PersonSymposium)
                .HasForeignKey(ps => ps.PersonId);

            modelBuilder.Entity<PersonSymposium>()
                .HasOne(ps => ps.Symposium)
                .WithMany(s => s.PersonSymposium)
                .HasForeignKey(ps => ps.SymposiumId);

            modelBuilder.Entity<WorkshopSymposium>()
                .HasKey(ws => new { ws.SymposiumId, ws.WorkshopId});

            modelBuilder.Entity<WorkshopSymposium>()
                .HasOne(ws => ws.Workshop)
                .WithMany(w => w.WorkshopSymposium)
                .HasForeignKey(ws => ws.WorkshopId);

            modelBuilder.Entity<WorkshopSymposium>()
                .HasOne(ws => ws.Symposium)
                .WithMany(s => s.WorkshopSymposium)
                .HasForeignKey(ws => ws.SymposiumId);

            modelBuilder.Entity<SymposiumWorkshopEnrollment>()
                .HasKey(swe => new { swe.SymposiumId, swe.PersonId, swe.WorkshopId });

            modelBuilder.Entity<SymposiumWorkshopEnrollment>()
                .HasOne(swe => swe.Symposium)
                .WithMany(s => s.SymposiumWorkshopEnrollment)
                .HasForeignKey(swe => swe.SymposiumId);

            modelBuilder.Entity<SymposiumWorkshopEnrollment>()
                .HasOne(swe => swe.Person)
                .WithMany(p => p.SymposiumWorkshopEnrollment)
                .HasForeignKey(swe => swe.PersonId);

            modelBuilder.Entity<SymposiumWorkshopEnrollment>()
                .HasOne(swe => swe.Workshop)
                .WithMany(w => w.SymposiumWorkshopEnrollment)
                .HasForeignKey(swe => swe.WorkshopId);
        }
    }
}
