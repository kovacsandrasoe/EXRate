using EXRate.Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EXRate.Backend.Data
{
    public class EXRateContext : IdentityDbContext<AppUser>
    {
        public virtual DbSet<RateRecord> Records { get; set; }

        public EXRateContext(DbContextOptions<EXRateContext> ctx) : base(ctx)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();

            AppUser kovi = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "kovi91@gmail.com",
                EmailConfirmed = true,
                FirstName = "András",
                LastName = "Kovács",
                UserName = "kovi91@gmail.com",
                NormalizedUserName = "KOVI91@GMAIL.COM"
            };
            kovi.PasswordHash = ph.HashPassword(kovi, "Almafa123!!!");

            builder.Entity<AppUser>().HasData(kovi);
        }

    }
}
