using EatSomewhere.Data;
using Microsoft.EntityFrameworkCore;
using EatSomewhere.Users;

namespace EatSomewhere.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserSession> Sessions { get; set; }
    public DbSet<Assembly> Assemblies { get; set; }
    public DbSet<Ingredient?> Ingredients { get; set; }
    public DbSet<IngredientEntry> IngredientEntries { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<FoodEntry> FoodEntries { get; set; }
    public DbSet<Bill> Bills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Config.LoadConfig();
        optionsBuilder.UseNpgsql(Config.Instance?.dbConnectionString == null ? new Config().dbConnectionString : Config.Instance.dbConnectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().Navigation(x => x.Intolerances).AutoInclude();
        
        modelBuilder.Entity<UserSession>().HasKey(x => x.Id);

        modelBuilder.Entity<Assembly>().HasKey(x => x.Id);
        modelBuilder.Entity<Assembly>().Navigation(x => x.Users).AutoInclude();
        modelBuilder.Entity<Assembly>().HasMany(x => x.Users).WithMany();
        modelBuilder.Entity<Assembly>().Navigation(x => x.Admins).AutoInclude();
        modelBuilder.Entity<Assembly>().HasMany(x => x.Admins).WithMany();
        modelBuilder.Entity<Assembly>().Navigation(x => x.Pending).AutoInclude();
        modelBuilder.Entity<Assembly>().HasMany(x => x.Pending).WithMany();
        modelBuilder.Entity<Assembly>().HasMany(x => x.Ingredients).WithOne(x => x.Assembly);
        modelBuilder.Entity<Assembly>().HasMany(x => x.Foods).WithOne(x => x.Assembly);
        modelBuilder.Entity<Assembly>().HasMany(x => x.FoodEntries).WithOne(x => x.Assembly);
        
        modelBuilder.Entity<Food>().HasKey(x => x.Id);
        modelBuilder.Entity<Food>().Navigation(x => x.CreatedBy).AutoInclude();
        modelBuilder.Entity<Food>().Navigation(x => x.Assembly).AutoInclude();
        modelBuilder.Entity<Food>().Navigation(x => x.Ingredients).AutoInclude();
        modelBuilder.Entity<Food>().HasMany(x => x.Ingredients).WithMany(x => x.Foods);
        modelBuilder.Entity<Food>().Navigation(x => x.Tags).AutoInclude();
        modelBuilder.Entity<Food>().HasMany(x => x.Tags).WithMany();
        
        modelBuilder.Entity<FoodEntry>().HasKey(x => x.Id);
        modelBuilder.Entity<FoodEntry>().Navigation(x => x.Food).AutoInclude();
        modelBuilder.Entity<FoodEntry>().HasOne(x => x.Food).WithMany(x => x.FoodEntries);
        modelBuilder.Entity<FoodEntry>().Navigation(x => x.Participants).AutoInclude();
        modelBuilder.Entity<FoodEntry>().HasMany(x => x.Participants).WithOne(x => x.FoodEntry);
        modelBuilder.Entity<FoodEntry>().HasOne(x => x.CreatedBy).WithMany();
        modelBuilder.Entity<FoodEntry>().Navigation(x => x.CreatedBy).AutoInclude();
        modelBuilder.Entity<FoodEntry>().HasOne(x => x.PayedBy).WithMany(x => x.PayedFoodEntries);
        modelBuilder.Entity<FoodEntry>().Navigation(x => x.PayedBy).AutoInclude();
        modelBuilder.Entity<FoodEntry>().HasOne(x => x.Assembly).WithMany(x => x.FoodEntries);
        
        modelBuilder.Entity<FoodParticipant>().HasKey(x => x.Id);
        
        modelBuilder.Entity<Ingredient>().HasKey(x => x.Id);
        modelBuilder.Entity<Ingredient>().Navigation(x => x.Assembly).AutoInclude();
        modelBuilder.Entity<Ingredient>().Navigation(x => x.CreatedBy).AutoInclude();
        
        modelBuilder.Entity<IngredientEntry>().HasKey(x => x.Id);
        modelBuilder.Entity<IngredientEntry>().Navigation(x => x.Ingredient).AutoInclude();
        modelBuilder.Entity<IngredientEntry>().HasOne(x => x.Ingredient).WithMany(x=> x.IngredientEntries);

        modelBuilder.Entity<Bill>().HasOne(x => x.Recipient).WithMany(x => x.ReceivedBills);
        modelBuilder.Entity<Bill>().Navigation(x => x.Recipient).AutoInclude();
        modelBuilder.Entity<Bill>().HasOne(x => x.User).WithMany(x => x.Bills);
        modelBuilder.Entity<Bill>().Navigation(x => x.User).AutoInclude();
        modelBuilder.Entity<Bill>().HasOne(x => x.FoodEntry).WithMany(x => x.Bills);
        
        modelBuilder.Entity<Tag>().HasKey(x => x.Id);
    }
}