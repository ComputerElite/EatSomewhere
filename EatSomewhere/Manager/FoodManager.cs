using EatSomewhere.Data;
using EatSomewhere.Database;
using EatSomewhere.Server;
using EatSomewhere.Users;
using Microsoft.EntityFrameworkCore;

namespace EatSomewhere.Manager;

public class FoodManager
{
    public static List<Ingredient> GetIngredients(User user)
    {
        using var d = new AppDbContext();
        List<Ingredient> ingredients = new List<Ingredient>();
        foreach (Assembly assembly in d.Assemblies.Where(x => x.Users.Contains(user)).Include(x => x.Ingredients))
        {   
            ingredients.AddRange(assembly.Ingredients);
        }
        
        return ingredients;
    }
    
    public static List<Assembly> GetAssemblies(User user)   
    {
        using var d = new AppDbContext();
        d.Attach(user);
        return d.Assemblies.Where(a => a.Users.Contains(user)).ToList();
    }

    public static ApiResponse CreateAssembly(User user, Assembly toCreate)
    {
        Assembly assembly = new()
        {
            Name = toCreate.Name,
            Description = toCreate.Description,
            Users = new List<User>(),
            Admins = new List<User>()
        };
        
        using var d = new AppDbContext();
        d.Attach(user);
        assembly.Users.Add(user);
        assembly.Admins.Add(user);
        d.Assemblies.Add(assembly);
        d.SaveChanges();

        return new ApiResponse
        {
            CreatedId = assembly.Id,
            Success = true
        };
    }

    public static ApiResponse CreateIngredient(User user, Ingredient ingredient)
    {
        
        using var d = new AppDbContext();
        Assembly? a = d.Assemblies.FirstOrDefault(x => x.Id == ingredient.Assembly.Id);
        if (a == null)
        {
            return new ApiResponse
            {
                Success = false,
                Error = "Assembly not found"
            };
        }
        // Check if ingredient already exists and if yes, update it instead of creating it
        Ingredient? existingIngredient = d.Ingredients.FirstOrDefault(x => x.Id == ingredient.Id);
        if (existingIngredient != null)
        {
            existingIngredient.Name = ingredient.Name;
            existingIngredient.Amount = ingredient.Amount;
            existingIngredient.Cost = ingredient.Cost;
            existingIngredient.Unit = ingredient.Unit;
            d.Ingredients.Update(existingIngredient);
        }
        else
        {
            d.Ingredients.Add(ingredient);
        }
        d.SaveChanges();

        return new ApiResponse
        {
            CreatedId = ingredient.Id,
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
        
        if (!CanAdministrateAssembly(user, assembly))
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
    
    static bool CanAdministrateAssembly(User user, Assembly assembly)
    {
        return assembly.Admins.Contains(user);
    }

    public static ApiResponse DeleteIngredient(User user, string id)
    {
        using var d = new AppDbContext();
        var ingredient = d.Ingredients.FirstOrDefault(a => a.Id == id);
        if (ingredient == null)
        {
            return new ApiResponse
            {
                Success = false,
                Error = "Ingredient not found"
            };
        }
        
        if (!CanAdministrateAssembly(user, ingredient.Assembly))
        {
            return new ApiResponse
            {
                Success = false,
                Error = "User is not an admin of this assembly"
            };
        }
        
        d.Ingredients.Remove(ingredient);
        
        d.SaveChanges();
        
        return new ApiResponse
        {
            Success = true
        };
    }

    public static ApiResponse RequestJoinAssembly(User user, string name)
    {
        using var d = new AppDbContext();
        d.Attach(user);
        List<Assembly> foundAssemblies = d.Assemblies.Where(a => a.Name == name).ToList();

        foreach (Assembly a in foundAssemblies)
        {
            if (a.Pending.Any(x => x.Id == user.Id)) continue;
            a.Pending.Add(user);
        }

        d.SaveChanges();
        
        return new ApiResponse
        {
            Success = true,
            Error = "If the assembly exists a join requests has been issued. An Admin of the Assembly must approve it."
        };
    }

    public static ApiResponse ApproveJoinAssembly(User user, string assemblyId, string userId)
    {
        using var d = new AppDbContext();
        d.Attach(user);
        Assembly? foundAssembly = d.Assemblies.FirstOrDefault(a => a.Id == assemblyId);
        if (foundAssembly == null)
        {
            return new ApiResponse
            {
                Error = "Couldn't find assembly",
                Success = false
            };
        }
        
        if (!foundAssembly.Admins.Contains(user))
        {
            return new ApiResponse
            {
                Error = "User is not an admin of this assembly",
                Success = false
            };
        }
        
        User? foundUser = d.Users.FirstOrDefault(u => u.Id == userId);
        
        if (foundUser == null)
        {
            return new ApiResponse
            {
                Error = "Couldn't find user",
                Success = false
            };
        }
        
        if (!foundAssembly.Pending.Contains(foundUser))
        {
            return new ApiResponse
            {
                Error = "User has not requested to join this assembly",
                Success = false
            };
        }
        
        foundAssembly.Pending.Remove(foundUser);
        foundAssembly.Users.Add(foundUser);

        d.SaveChanges();
        
        return new ApiResponse
        {
            Success = true,
            Error = $"Successfully added {foundUser.Username} to {foundAssembly.Name}."
        };
    }

    public static ApiResponse RemoveUserFromAssembly(User user, string assemblyId, string userId)
    {
        using var d = new AppDbContext();
        d.Attach(user);
        Assembly? foundAssembly = d.Assemblies.FirstOrDefault(a => a.Id == assemblyId);
        if (foundAssembly == null)
        {
            return new ApiResponse
            {
                Error = "Couldn't find assembly",
                Success = false
            };
        }
        
        if (!foundAssembly.Admins.Contains(user))
        {
            return new ApiResponse
            {
                Error = "User is not an admin of this assembly",
                Success = false
            };
        }
        
        User? foundUser = foundAssembly.Users.FirstOrDefault(u => u.Id == userId);
        if (foundUser == null)
        {
            foundUser = foundAssembly.Admins.FirstOrDefault(x => x.Id == userId);
        }
        
        if (foundUser == null)
        {
            return new ApiResponse
            {
                Error = "Couldn't find user in assembly",
                Success = false
            };
        }
        
        // admins CAN be removed
        foundAssembly.Users.Remove(foundUser);
        foundAssembly.Admins.Remove(foundUser);

        d.SaveChanges();
        
        return new ApiResponse
        {
            Success = true,
            Error = $"Successfully removed {foundUser.Username} from {foundAssembly.Name}."
        };
    }

    public static Assembly? GetAssembly(User user, string id)
    {
        using var d = new AppDbContext();
        return d.Assemblies.FirstOrDefault(a => a.Id == id && a.Users.Contains(user));
    }
    
    public static Ingredient? GetIngredient(User user, string id)
    {
        using var d = new AppDbContext();
        return d.Ingredients.FirstOrDefault(a => a.Id == id && a.Assembly.Users.Contains(user));
    }
}