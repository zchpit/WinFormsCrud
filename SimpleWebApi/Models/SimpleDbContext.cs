using Microsoft.EntityFrameworkCore;

namespace SimpleWebApi.Model
{
    public class SimpleDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<UserCase> UserCases { get; set; }

        public SimpleDbContext(DbContextOptions<SimpleDbContext> options)
      : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ConnStr");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(a =>
            {
                a.HasKey(u => u.Id).IsClustered();
            });
            modelBuilder.Entity<Case>(a =>
            {
                a.HasKey(u => u.Id).IsClustered();
            });

            modelBuilder.Entity<UserCase>(a =>
            {
                a.HasKey(e => new { e.UserId, e.CaseId });
                a.HasOne(e => e.Case).WithMany(e => e.UserCases).HasForeignKey(e => e.CaseId);
                a.HasOne(e => e.User).WithMany(e => e.UserCases).HasForeignKey(e => e.UserId);
            });
        }
    }
}
