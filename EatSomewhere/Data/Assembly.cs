using System.ComponentModel.DataAnnotations.Schema;
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
    public List<Ingredient> Ingredients;
}