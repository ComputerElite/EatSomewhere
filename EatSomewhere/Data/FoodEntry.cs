using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EatSomewhere.Users;

namespace EatSomewhere.Data;

public class FoodEntry
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; }
    public Food Food { get; set; }
    public int EstimatedCost => (int)double.Ceiling(Food.EstimatedCost / (double)Food.PersonCount * PersonCount);
    public int Cost { get; set; }

    public int CostPerPerson => (int)double.Ceiling(Cost / (double)PersonCount);
    
    public int PersonCount => Participants.Sum(x => x.AdditionalPersons + 1);
    public List<FoodParticipant> Participants { get; set; }
    public User PayedBy { get; set; }
    [JsonIgnore]
    public Assembly Assembly { get; set; }

    public User CreatedBy { get; set; }
}