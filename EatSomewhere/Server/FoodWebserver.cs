using System.Text.Json;
using ComputerUtils.Logging;
using ComputerUtils.Webserver;
using EatSomewhere.Data;
using EatSomewhere.Database;
using EatSomewhere.Manager;
using EatSomewhere.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EatSomewhere.Server;

public class FoodWebserver
{
    public static void RegisterRESTForType<T>(string path, HttpServer server, Func<User, string, T?> getFunction, Func<User, T, ApiResponse<T>> postFunction, Func<User, string, ApiResponse> deleteFunction)
    {
        server.AddRoute("GET", path, request =>
        {
            request.allowAllOrigins = true;
            User? user = UserManagementServer.GetUserBySession(request);
            if (user == null)
            {
                ApiError.SendUnauthorized(request);
                return true;
            }

            string id = request.pathDiff;
            T? assembly = getFunction(user, id);
            request.SendString(JsonSerializer.Serialize(assembly), "application/json", assembly != null ? 200 : 404);
            return true;
        }, true);
        server.AddRoute("POST", path, request =>
        {
            request.allowAllOrigins = true;
            User? user = UserManagementServer.GetUserBySession(request);
            if (user == null)
            {
                ApiError.SendUnauthorized(request);
                return true;
            }

            T? assembly;
            try
            {
                assembly = JsonSerializer.Deserialize<T>(request.bodyString);
                if(assembly == null) throw new Exception();
            } catch
            {
                ApiError.MalformedRequest(request);
                return true;
            }
            SendResponse(request, postFunction(user, assembly));
            return true;
        });
        server.AddRoute("DELETE", path, request =>
        {
            request.allowAllOrigins = true;
            User? user = UserManagementServer.GetUserBySession(request);
            if (user == null)
            {
                ApiError.SendUnauthorized(request);
                return true;
            }
            string id = request.pathDiff;
            SendResponse(request, deleteFunction(user, id));
            return true;
        }, true);
    }
    public static void AddFoodRoutes(HttpServer server)
    {
        /// Billing
        RegisterPagedListForType("/api/v1/bills/", server, BillManager.GetBills);
        server.AddRoute("GET", "/api/v1/totals/", request =>
        {
            request.allowAllOrigins = true;
            User? user = UserManagementServer.GetUserBySession(request);
            if (user == null)
            {
                ApiError.SendUnauthorized(request);
                return true;
            }
            string id = request.pathDiff;

            Assembly? assembly;
            using (var d = new AppDbContext())
            {
                d.Attach(user);
                    assembly = d.Assemblies.Include(x => x.Users).FirstOrDefault(x => x.Id == id);
                    if (assembly == null)
                    {
                    ApiError.SendNotFound(request);
                    return true;
                }
            }

            List<Bill> bills = [];
            foreach (User u in assembly.Users)
            {
                bills.AddRange(BillManager.GetTotalBills(u, id));
            }
            request.SendString(JsonSerializer.Serialize(bills), "application/json", 200);

            //request.SendString(JsonSerializer.Serialize(BillManager.GetTotalBills(user, id)), "application/json", 200);
            return true;
        }, true);
        
        ////// Food Entries //////
        /// DELETE archives food entries
        //api/v1/foodentries/<assemblyId>?skip=<int>&count=<int>
        RegisterPagedListForType("/api/v1/foodentries/", server, FoodManager.GetFoodEntries);
        RegisterRESTForType("/api/v1/foodentry/", server, FoodManager.GetFoodEntry, FoodManager.CreateFoodEntry, FoodManager.DeleteFoodEntry);
        
        ////// Food //////
        /// DELETE archives food
        RegisterListForType("/api/v1/foods", server, FoodManager.GetFoods);
        RegisterRESTForType("/api/v1/food/", server, FoodManager.GetFood, FoodManager.CreateFood, FoodManager.DeleteFood);

        ////// INGREDIENTS //////
        /// DO NOT CHANGE ORDER, THIS IS IMPORTANT FOR MY CURSED HTTP ROUTE MATCHING
        /// DELETE archives ingredients
        RegisterListForType("/api/v1/ingredients", server, FoodManager.GetIngredients);
        RegisterRESTForType("/api/v1/ingredient/", server, FoodManager.GetIngredient, FoodManager.CreateIngredient, FoodManager.DeleteIngredient);

        ////// ASSEMBLIES //////
        server.AddRoute("POST", "/api/v1/joinassembly", request =>
        {
            request.allowAllOrigins = true;
            User? user = UserManagementServer.GetUserBySession(request);
            if (user == null)
            {
                ApiError.SendUnauthorized(request);
                return true;
            }
            Assembly? assembly;
            try
            {
                assembly = JsonSerializer.Deserialize<Assembly>(request.bodyString);
                if (assembly == null) throw new Exception();
            }
            catch
            {
                ApiError.MalformedRequest(request);
                return true;
            }
            SendResponse(request, FoodManager.RequestJoinAssembly(user, assembly.Name));
            return true;
        });
        server.AddRoute("POST", "/api/v1/removeuser", request =>
        {
            request.allowAllOrigins = true;
            User? user = UserManagementServer.GetUserBySession(request);
            if (user == null)
            {
                ApiError.SendUnauthorized(request);
                return true;
            }
            UserAssemblyRequest? assembly;
            try
            {
                assembly = JsonSerializer.Deserialize<UserAssemblyRequest>(request.bodyString);
                if (assembly == null) throw new Exception();
            }
            catch
            {
                ApiError.MalformedRequest(request);
                return true;
            }
            SendResponse(request, FoodManager.RemoveUserFromAssembly(user, assembly.AssemblyId, assembly.UserId));
            return true;
        });
        server.AddRoute("POST", "/api/v1/adduser", request =>
        {
            request.allowAllOrigins = true;
            User? user = UserManagementServer.GetUserBySession(request);
            if (user == null)
            {
                ApiError.SendUnauthorized(request);
                return true;
            }
            UserAssemblyRequest? assembly;
            try
            {
                assembly = JsonSerializer.Deserialize<UserAssemblyRequest>(request.bodyString);
                if (assembly == null) throw new Exception();
            }
            catch
            {
                ApiError.MalformedRequest(request);
                return true;
            }
            SendResponse(request, FoodManager.ApproveJoinAssembly(user, assembly.AssemblyId, assembly.UserId));
            return true;
        });
        
        server.AddRoute("POST", "/api/v1/promote", request =>
        {
            request.allowAllOrigins = true;
            User? user = UserManagementServer.GetUserBySession(request);
            if (user == null)
            {
                ApiError.SendUnauthorized(request);
                return true;
            }
            UserAssemblyRequest? assembly;
            try
            {
                assembly = JsonSerializer.Deserialize<UserAssemblyRequest>(request.bodyString);
                if (assembly == null) throw new Exception();
            }
            catch
            {
                ApiError.MalformedRequest(request);
                return true;
            }

            SendResponse(request, FoodManager.PromoteUserToAdmin(user, assembly.AssemblyId, assembly.UserId));
            return true;
        });
        RegisterListForType("/api/v1/assemblies", server, FoodManager.GetAssemblies);
        RegisterRESTForType("/api/v1/assembly/", server, FoodManager.GetAssembly, FoodManager.CreateAssembly, FoodManager.DeleteAssembly);
    }

    private static void SendResponse<T>(ServerRequest request, ApiResponse<T> response)
    {
        request.SendString(JsonSerializer.Serialize(response), "application/json", response.Success ? 200 : 400);
    }
    private static void SendResponse(ServerRequest request, ApiResponse response)
    {
        request.SendString(JsonSerializer.Serialize(response), "application/json", response.Success ? 200 : 400);
    }
    
    private static void RegisterPagedListForType<T>(string apiPath, HttpServer server, Func<User, string, int, int, List<T>> listMethod)
    {        
        server.AddRoute("GET", apiPath, request =>
        {
            request.allowAllOrigins = true;
            User? user = UserManagementServer.GetUserBySession(request);
            if (user == null)
            {
                ApiError.SendUnauthorized(request);
                return true;
            }
            int skip = 0;
            int count = 0;
            if(request.queryString.Get("skip") != null)
            {
                try
                {
                    skip = int.Parse(request.queryString.Get("skip") ?? string.Empty);
                }
                catch
                {
                    // ignored
                }
            }
            if(request.queryString.Get("count") != null)
            {
                try
                {
                    count = int.Parse(request.queryString.Get("count") ?? string.Empty);
                }
                catch
                {
                    // ignored
                }
            }
            Logger.Log("Skip: " + skip + " Count: " + count+" Assembly: " + request.pathDiff);
            request.SendString(JsonSerializer.Serialize(listMethod(user, request.pathDiff, skip, count)), "application/json");
            return true;
        }, true);
    }

    private static void RegisterListForType<T>(string apiPath, HttpServer server, Func<User, List<T>> listMethod)
    {        
        server.AddRoute("GET", apiPath, request =>
        {
            request.allowAllOrigins = true;
            User? user = UserManagementServer.GetUserBySession(request);
            if (user == null)
            {
                ApiError.SendUnauthorized(request);
                return true;
            }
            request.SendString(JsonSerializer.Serialize(listMethod(user)), "application/json");
            return true;
        });
    }
}

public class UserAssemblyRequest
{
    public string AssemblyId { get; set; }
    public string UserId { get; set; }
}