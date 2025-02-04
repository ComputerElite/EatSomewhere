using System.ComponentModel.DataAnnotations.Schema;

namespace EatSomewhere.Data;

public class Food
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public String Name { get; set; }
    public Assembly Assembly { get; set; }
    public List<IngredientEntry> Ingredients { get; set; }
    public List<Tag> Tags { get; set; }
    public int PersonCount { get; set; }
    public string Rezept { get; set; }
}