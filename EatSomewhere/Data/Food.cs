using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EatSomewhere.Users;

namespace EatSomewhere.Data;

public class Food
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public string Name { get; set; }
    public User CreatedBy { get; set; }
    public Assembly Assembly { get; set; }
    public List<IngredientEntry> Ingredients { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();
    public int PersonCount { get; set; } = 1;
    
    [NotMapped]
    public long EstimatedCost
    {
        get
        {
            return Ingredients.Sum(x => x.EstimatedCost);
        }
    }
    public string Recipe { get; set; }
    public bool Archived { get; set; } = false;
    [JsonIgnore]
    public IEnumerable<FoodEntry>? FoodEntries { get; set; }
}