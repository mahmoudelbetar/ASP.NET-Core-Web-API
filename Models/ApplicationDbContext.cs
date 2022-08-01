using Microsoft.EntityFrameworkCore;

namespace ParkyAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=ParkyAPI;Trusted_Connection=True");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<NationalPark> NationalPark { get; set; }
        public DbSet<Trail> Trails { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
