using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EatSomewhere.Data;

namespace EatSomewhere.Users;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public string Username { get; set; }
    [JsonIgnore]
    public string PasswordHash { get; set; }
    [JsonIgnore]
    public string Salt { get; set; }
    [JsonIgnore]
    public bool TwoFactorEnabled { get; set; }
    public List<Ingredient> Intolerances { get; set; }
}