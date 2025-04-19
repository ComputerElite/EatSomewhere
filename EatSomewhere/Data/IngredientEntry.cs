using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EatSomewhere.Data;

public class IngredientEntry
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public Ingredient Ingredient { get; set; }
    public double Amount { get; set; }
    [NotMapped] public int EstimatedCost => (int)double.Round(Ingredient.Cost / Ingredient.Amount * Amount);
    [JsonIgnore] public List<Food>? Foods { get; set; } = new();
}