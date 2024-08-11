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
            //here you can MAP Your Models/Entities, but i am going to show you something more interesting. so keep up. 
            //modelBuilder.Configurations.Add(new UsersMap());

            modelBuilder.Entity<UserCase>().HasKey(e => new { e.UserId, e.CaseId });
            modelBuilder.Entity<User>().HasKey(e => e.Id); //.HasMany(a => a.UserCases);
            modelBuilder.Entity<Case>().HasKey(e => e.Id); //.HasMany(a => a.UserCases);

            /*TODO*/
            /*
             Create more sofisticated database model that that.

                modelBuilder.Entity<User>(a =>
                {
                    a.HasKey(u => u.Id).IsClustered();
                    a.HasMany<UserCase>().WithOne(a => a.User);
                });
             */
        }
    }
}
