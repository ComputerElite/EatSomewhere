using Microsoft.EntityFrameworkCore;
using EatSomewhere.Users;

namespace EatSomewhere.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserSession> Sessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Config.LoadConfig();
        optionsBuilder.UseSqlite(Config.Instance.dbConnectionString == null ? "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "database.db" : Config.Instance.dbConnectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<UserSession>().HasKey(x => x.Id);
    }
}