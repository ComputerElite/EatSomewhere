using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EatSomewhere.Users;

namespace EatSomewhere.Data;

public class FoodParticipant
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id;
    public User User;
    public int AdditionalPersons;
    [JsonIgnore]
    public Food Food;
    [JsonIgnore]
    public Assembly Assembly;
}