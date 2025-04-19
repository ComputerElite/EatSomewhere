using ComputerUtils.Logging;
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
            ingredients.AddRange(assembly.Ingredients.Where(x => !x.Archived));
        }
        return ingredients;
    }

    public static List<Food> GetFoods(User user)
    {
        using var d = new AppDbContext();
        List<Food> foods = new List<Food>();
        foreach (Assembly assembly in d.Assemblies.Where(x => x.Users.Contains(user)).Include(x => x.Foods))
        {
            foreach (Food food in assembly.Foods.Where(x => !x.Archived))
            {
                Logger.Log(food.Name);
                foods.Add(food);
            }
        }
        return foods;
    }
    
    public static List<Assembly> GetAssemblies(User user)   
    {
        using var d = new AppDbContext();
        d.Attach(user);
        return d.Assemblies.Where(a => a.Users.Contains(user)).ToList();
    }

    public static ApiResponse<Assembly> CreateAssembly(User user, Assembly toCreate)
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

        return new ApiResponse<Assembly>
        {
            CreatedId = assembly.Id,
            Success = true,
            Data = assembly
        };
    }
    
    

    public static ApiResponse<Food> CreateFood(User user, Food food)
    {
        using var d = new AppDbContext();
        d.Attach(user);
        Assembly? a = d.Assemblies.FirstOrDefault(x => x.Id == food.Assembly.Id);
        if (a == null)
        {
            return new ApiResponse<Food>
            {
                Success = false,
                Error = "Assembly not found"
            };
        }
        
        // Check if food already exists and if yes, update it instead of creating it
        Food? existingIngredient = d.Foods.FirstOrDefault(x => x.Id == food.Id);
        if (existingIngredient != null)
        {
            if (existingIngredient.CreatedBy.Id != user.Id && !CanAdministrateAssembly(user, a))
            {
                return new ApiResponse<Food>
                {
                    Success = false,
                    Error = "You are not allowed to edit this food"
                };
            }
            existingIngredient.Archived = true;
        }

        foreach (IngredientEntry ingredientEntry in food.Ingredients)
        {
            ingredientEntry.Id = null;
            ingredientEntry.Ingredient = d.Ingredients.FirstOrDefault(x => x != null && x.Id == ingredientEntry.Ingredient.Id);
        }

        food.Id = null;
        food.CreatedBy = user;
        food.Assembly = a;
        d.Foods.Add(food);
        d.SaveChanges();

        return new ApiResponse<Food>
        {
            CreatedId = food.Id,
            Success = true,
            Data = food
        };
    }

    public static ApiResponse<Ingredient> CreateIngredient(User user, Ingredient ingredient)
    {
        using var d = new AppDbContext();
        d.Attach(user);
        Assembly? a = d.Assemblies.FirstOrDefault(x => x.Id == ingredient.Assembly.Id);
        if (a == null)
        {
            return new ApiResponse<Ingredient>
            {
                Success = false,
                Error = "Assembly not found"
            };
        }
        
        // Check if ingredient already exists and if yes, update it instead of creating it
        Ingredient? existingIngredient = d.Ingredients.FirstOrDefault(x => x.Id == ingredient.Id);
        if (existingIngredient != null)
        {
            if (existingIngredient.CreatedBy.Id != user.Id && !CanAdministrateAssembly(user, a))
            {
                return new ApiResponse<Ingredient>
                {
                    Success = false,
                    Error = "You are not allowed to edit this ingredient"
                };
            }
            existingIngredient.Archived = true;
        }

        IEnumerable<Food> needsUpdating = d.Foods.Where(x => x.Ingredients.Any(x => x.Ingredient.Id == ingredient.Id));
        ingredient.Id = null;
        ingredient.Assembly = a;
        ingredient.CreatedBy = user;
        d.Ingredients.Add(ingredient);
        d.SaveChanges();
        foreach (Food food in needsUpdating)
        {
            Logger.Log("Updating food " + food.Name + " with new ingredient entry of " + ingredient.Name);
            food.Archived = true;
            d.SaveChanges();
            // DO NOT SAVE AFTERWARDS
            // Create copy of food which has updated ingredients
            food.Archived = false;
            food.Ingredients.Where(x => x.Ingredient.Id == ingredient.Id).ToList().ForEach(x => x.Ingredient = ingredient);
            CreateFood(food.CreatedBy, food);
        }

        return new ApiResponse<Ingredient>
        {
            CreatedId = ingredient.Id,
            Success = true,
            Data = ingredient
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
        ingredient.Archived = true;
        
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

    public static Food? GetFood(User user, string id)
    {
        using var d = new AppDbContext();
        return d.Foods.FirstOrDefault(a => a.Id == id && a.Assembly.Users.Contains(user));
    }

    public static ApiResponse DeleteFood(User user, string id)
    {
        using var d = new AppDbContext();
        var food = d.Foods.FirstOrDefault(a => a.Id == id);
        if (food == null)
        {
            return new ApiResponse
            {
                Success = false,
                Error = "Food not found"
            };
        }
        
        if (!CanAdministrateAssembly(user, food.Assembly))
        {
            return new ApiResponse
            {
                Success = false,
                Error = "User is not an admin of this assembly"
            };
        }

        food.Archived = true;
        
        d.SaveChanges();

        return new ApiResponse
        {
            Success = true
        };
    }
}