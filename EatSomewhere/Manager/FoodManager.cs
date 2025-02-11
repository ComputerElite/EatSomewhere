using EatSomewhere.Data;
using EatSomewhere.Database;
using EatSomewhere.Server;
using EatSomewhere.Users;

namespace EatSomewhere.Manager;

public class FoodManager
{
    public static List<Assembly> GetAssemblies(User user)
    {
        using var d = new AppDbContext();
        return d.Assemblies.Where(a => a.Users.Contains(user)).ToList();
    }

    public static ApiResponse CreateAssembly(User user, Assembly toCreate)
    {
        Assembly assembly = new()
        {
            Name = toCreate.Name,
            Description = toCreate.Description,
            Admins = new List<User> { user },
            Users = new List<User> { user }
        };
        
        using var d = new AppDbContext();
        d.Attach(user);
        d.Assemblies.Add(assembly);
        d.SaveChanges();

        return new ApiResponse
        {
            CreatedId = assembly.Id,
            Success = true
        };
    }

    public static ApiResponse DeleteAssembly(User user, string Id)
    {
        using var d = new AppDbContext();
        var assembly = d.Assemblies.FirstOrDefault(a => a.Id == Id);
        if (assembly == null)
        {
            return new ApiResponse
            {
                Success = false,
                Error = "Assembly not found"
            };
        }
        
        if (!assembly.Admins.Contains(user))
        {
            return new ApiResponse
            {
                Success = false,
                Error = "User is not an admin of this assembly"
            };
        }
        
        d.Assemblies.Remove(assembly);
        
        d.SaveChanges();
        
        return new ApiResponse
        {
            Success = true
        };
    }

    public static Assembly? GetAssembly(User user, string id)
    {
        using var d = new AppDbContext();
        return d.Assemblies.FirstOrDefault(a => a.Id == id && a.Users.Contains(user));
    }
}