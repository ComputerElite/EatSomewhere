using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EatSomewhere.Users;

namespace EatSomewhere.Data;

public class Assembly
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public List<User> Users { get; set; }= new();

    public List<User> Pending { get; set; } = new();
    public string Name { get; set; }
    public string Description { get; set; }
    public List<User> Admins { get; set; }= new();
    [JsonIgnore] public IEnumerable<Food> Foods { get; set; }
    [JsonIgnore] public IEnumerable<FoodEntry> FoodEntries { get; set; }

    [JsonIgnore] public IEnumerable<Ingredient> Ingredients { get; set; }
}