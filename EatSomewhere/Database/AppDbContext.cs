using EatSomewhere.Data;
using Microsoft.EntityFrameworkCore;
using EatSomewhere.Users;

namespace EatSomewhere.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserSession> Sessions { get; set; }
    public DbSet<Assembly> Assemblies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Config.LoadConfig();
        optionsBuilder.UseSqlite(Config.Instance.dbConnectionString == null ? "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "database.db" : Config.Instance.dbConnectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().Navigation(x => x.Intolerances).AutoInclude();
        
        modelBuilder.Entity<UserSession>().HasKey(x => x.Id);

        modelBuilder.Entity<Assembly>().HasKey(x => x.Id);
        modelBuilder.Entity<Assembly>().Navigation(x => x.Users).AutoInclude();
        modelBuilder.Entity<Assembly>().Navigation(x => x.Admins).AutoInclude();
        modelBuilder.Entity<Assembly>().Navigation(x => x.Pending).AutoInclude();
        
        modelBuilder.Entity<Food>().HasKey(x => x.Id);
        modelBuilder.Entity<Food>().Navigation(x => x.Assembly).AutoInclude();
        modelBuilder.Entity<Food>().Navigation(x => x.Ingredients).AutoInclude();
        modelBuilder.Entity<Food>().Navigation(x => x.Tags).AutoInclude();
        
        modelBuilder.Entity<FoodEntry>().HasKey(x => x.Id);
        modelBuilder.Entity<FoodEntry>().Navigation(x => x.Food).AutoInclude();
        modelBuilder.Entity<FoodEntry>().Navigation(x => x.Participants).AutoInclude();
        modelBuilder.Entity<FoodEntry>().Navigation(x => x.PayedBy).AutoInclude();
        
        modelBuilder.Entity<FoodParticipant>().HasKey(x => x.Id);
        
        modelBuilder.Entity<Ingredient>().HasKey(x => x.Id);
        
        modelBuilder.Entity<IngredientEntry>().HasKey(x => x.Id);
        modelBuilder.Entity<IngredientEntry>().Navigation(x => x.Ingredient).AutoInclude();
        
        modelBuilder.Entity<Tag>().HasKey(x => x.Id);
    }
}