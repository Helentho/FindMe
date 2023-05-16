using Microsoft.EntityFrameworkCore;

namespace FindMe.Models
{
    public class ApplicationContext : DbContext
    {
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => {
                entity.HasIndex(u => new { u.Email, u.PhoneNumber }).IsUnique();
            });

            modelBuilder.Entity<User>().Property(b => b.CreatedDate)
                                       .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Person>()
                .HasOne(s => s.User)
                .WithMany(g => g.People)
                .HasForeignKey(s => s.CurrentUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Person> People { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
