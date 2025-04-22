using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EatSomewhere.Data;

public class IngredientEntry
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public Ingredient Ingredient { get; set; }
    public decimal Amount { get; set; }
    [NotMapped] public long EstimatedCost => (long)decimal.Round(Ingredient.Cost / Ingredient.Amount * Amount);
    [JsonIgnore] public List<Food>? Foods { get; set; } = new();
}