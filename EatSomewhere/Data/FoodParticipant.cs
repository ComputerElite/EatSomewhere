using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EatSomewhere.Users;

namespace EatSomewhere.Data;

public class FoodParticipant
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public User? User { get; set; }
    public int AdditionalPersons { get; set; }
    [JsonIgnore]
    public FoodEntry FoodEntry { get; set; }
    [NotMapped]
    [JsonIgnore]
    public int Persons => AdditionalPersons + 1;
}