using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EatSomewhere.Users;

namespace EatSomewhere.Data;

public class Bill
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string? Id { get; set; }
    /// <summary>
    /// Person which ows money
    /// </summary>
    public User? User { get; set; }
    /// <summary>
    /// The person which receives the money
    /// </summary>
    public User Recipient { get; set; }
    [JsonIgnore]
    public FoodEntry FoodEntry { get; set; }
    public int Amount { get; set; }
    public int Persons { get; set; } = 1;
}