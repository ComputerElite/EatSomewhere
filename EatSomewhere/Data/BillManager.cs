using EatSomewhere.Database;
using EatSomewhere.Users;

namespace EatSomewhere.Data;

public class BillManager
{
    public static List<Bill> GetTotalBills(User user, string assemblyId, bool first = true)
    {
        using var d = new AppDbContext();
        List<Bill> total = d.Bills
            .Where(x => x.User.Id == user.Id && x.FoodEntry.Assembly.Id == assemblyId)
            .GroupBy(x => x.Recipient)
            .Select(x => new Bill
            {
                Amount = x.Sum(y => y.Amount),
                Recipient = x.Key,
                User = user,
                FoodEntry = x.First().FoodEntry,
            }).ToList();
        if (first)
        {
            foreach (var bill in total)
            {
                Bill? theirBill = d.Bills
                    .Where(x => x.User == bill.Recipient && x.Recipient.Id == user.Id && x.FoodEntry.Assembly.Id == assemblyId)
                    .GroupBy(x => x.Recipient)
                    .Select(x => new Bill
                    {
                        Amount = x.Sum(y => y.Amount),
                        Recipient = x.Key,
                        User = user,
                        FoodEntry = x.First().FoodEntry,
                    }).ToList().FirstOrDefault();
                bill.Amount -= theirBill?.Amount ?? 0;
            }
        }

        return total;
    }

    public static List<Bill> GetBills(User user, string assemblyId, int skip, int count)
    {
        using var d = new AppDbContext();
        List<Bill> total = d.Bills
            .OrderByDescending(x => x.Date)
            .Where(x => (x.User.Id == user.Id || x.Recipient.Id == user.Id) && x.FoodEntry.Assembly.Id == assemblyId)
            .Skip(skip)
            .Take(count)
            .ToList();

        return total;
    }
}