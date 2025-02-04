using System.ComponentModel.DataAnnotations.Schema;
using EatSomewhere.Users;

namespace EatSomewhere.Data;

public class FoodParticipant
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id;
    public User User;
    public Food Food;
    public Assembly Assembly;
    public int AdditionalPersons;
}