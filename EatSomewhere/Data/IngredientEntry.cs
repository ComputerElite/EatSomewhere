using System.ComponentModel.DataAnnotations.Schema;

namespace EatSomewhere.Data;

public class IngredientEntry
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public Ingredient Ingredient { get; set; }
    public double Amount { get; set; }
    public Unit Unit { get; set; }
}