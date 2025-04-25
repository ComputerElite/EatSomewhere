using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EatSomewhere.Users;

namespace EatSomewhere.Data;

public class FoodEntry
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public string? Comment { get; set; }
    public Food Food { get; set; }
    public long EstimatedCost => (long)decimal.Ceiling(Food.EstimatedCost / (decimal)Food.PersonCount * PersonCount);
    public long Cost { get; set; }

    public long CostPerPerson => (long)decimal.Ceiling(Cost / (decimal)Math.Max(1, PersonCount));
    
    public int PersonCount => Participants.Sum(x => x.Persons);
    public List<FoodParticipant> Participants { get; set; }
    public User? PayedBy { get; set; }
    public Assembly Assembly { get; set; }
    public User CreatedBy { get; set; }
    public List<Bill> Bills { get; set; }

    public List<Bill> CalculateBills()
    {
        List<Bill> bills = Participants.Select(x => new Bill
        {
            User = x.User ?? null,
            Amount = CostPerPerson * x.Persons,
            Persons = x.Persons,
            Recipient = PayedBy,
            Date = Date
        }).ToList();
        int i = 0;
        while (bills.Sum(x => x.Amount) > Cost)
        {
            bills[i % bills.Count].Amount -= 1;
            i++;
        }
        return bills;
    }
}