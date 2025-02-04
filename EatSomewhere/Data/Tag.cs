using System.ComponentModel.DataAnnotations.Schema;

namespace EatSomewhere.Data;

public class Tag
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public string Name { get; set; }
    public Assembly Assembly { get; set; }
}