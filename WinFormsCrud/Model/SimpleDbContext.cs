using System.Data.Entity;

namespace WinFormsCrud.Model
{
    public class SimpleDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<UserCase> UserCases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCase>().HasKey(e => new { e.UserId, e.CaseId });
            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<Case>().HasKey(e => e.Id);
        }
    }
}
