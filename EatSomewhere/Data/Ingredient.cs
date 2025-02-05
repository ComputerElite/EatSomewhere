using System.ComponentModel.DataAnnotations.Schema;

namespace EatSomewhere.Data;

public class Ingredient
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public string Name { get; set; }
    public double Cost { get; set; }
    public double Amount { get; set; }
    public Unit Unit { get; set; }
    public Assembly Assembly { get; set; }
}