using System.ComponentModel.DataAnnotations.Schema;
using EatSomewhere.Users;

namespace EatSomewhere.Data;

public class FoodEntry
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; }
    public Food Food { get; set; }
    public int Cost { get; set; }
    public int CostPerPerson { get; set; }
    public List<FoodParticipant> Participants { get; set; }
    public User PayedBy { get; set; }
}