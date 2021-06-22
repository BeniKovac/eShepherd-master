using web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public class eShepherdContext : IdentityDbContext<ApplicationUser>
    {
        public eShepherdContext(DbContextOptions<eShepherdContext> options) : base(options)
        {
        }
        public DbSet<Ovca> Ovce { get; set; }
        public DbSet<Oven> Ovni { get; set; }
        public DbSet<Creda> Crede { get; set; }
        public DbSet<Jagenjcek> Jagenjcki { get; set; }
        public DbSet<Gonitev> Gonitve { get; set; }
        public DbSet<Kotitev> Kotitve { get; set; }
    
     protected override void OnModelCreating(ModelBuilder modelBuilder)
        { //overridamo, da bo ime course in ne courses (kako vplivamo na obnašanje frameworka)
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ovca>().ToTable("Ovca");
            modelBuilder.Entity<Oven>().ToTable("Oven");
            modelBuilder.Entity<Jagenjcek>().ToTable("Jagenjcek");
            modelBuilder.Entity<Kotitev>().ToTable("Kotitev");
            modelBuilder.Entity<Gonitev>().ToTable("Gonitev");
            modelBuilder.Entity<Creda>().ToTable("Crede");
        }
}
}