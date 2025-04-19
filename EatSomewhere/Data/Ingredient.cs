using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EatSomewhere.Users;

namespace EatSomewhere.Data;

public class Ingredient
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public User CreatedBy { get; set; }
    public string Name { get; set; }
    /// <summary>
    /// Cents
    /// </summary>
    public int Cost { get; set; }
    public double Amount { get; set; }
    public Unit Unit { get; set; }
    public Assembly Assembly { get; set; }
    public bool Archived { get; set; } = false;

    [JsonIgnore] public List<IngredientEntry> IngredientEntries { get; set; } = new();
}