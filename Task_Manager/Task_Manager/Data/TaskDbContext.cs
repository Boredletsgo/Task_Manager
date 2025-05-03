using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task_Manager.Models;

namespace Task_Manager.Data
{
    public class TaskDbContext : IdentityDbContext<AppUser>
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Override the default data types to work with MySQL
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                // Change nvarchar(max) to TEXT for MySQL
                entity.Property(r => r.ConcurrencyStamp)
                      .HasColumnType("TEXT"); // Use TEXT for MySQL
            });

            modelBuilder.Entity<TaskItem>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
