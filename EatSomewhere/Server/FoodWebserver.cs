using System.Text.Json;
using ComputerUtils.Webserver;
using EatSomewhere.Data;
using EatSomewhere.Manager;
using EatSomewhere.Users;
using Microsoft.Extensions.Caching.Memory;

namespace EatSomewhere.Server;

public class FoodWebserver
{
    public static void RegisterRESTForType<T>(string path, HttpServer server, Func<User, string, T?> getFunction, Func<User, T, ApiResponse> postFunction, Func<User, string, ApiResponse> deleteFunction)
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
            request.SendString(JsonSerializer.Serialize(getFunction.Invoke(user, id)), "application/json");
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
            request.SendString(JsonSerializer.Serialize(postFunction(user, assembly)), "application/json");
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
            request.SendString(JsonSerializer.Serialize(deleteFunction(user, id)), "application/json");
            return true;
        }, true);
    }
    public static void AddFoodRoutes(HttpServer server)
    {
        ////// INGREDIENTS //////
        /// DO NOT CHANGE ORDER, THIS IS IMPORTANT FOR MY CURSED HTTP ROUTE MATCHING
        RegisterListForType("/api/v1/ingredients", server, FoodManager.GetIngredients);
        RegisterRESTForType("/api/v1/ingredient/", server, FoodManager.GetIngredient, FoodManager.CreateIngredient, FoodManager.DeleteIngredient);

        ////// ASSEMBLIES //////
        RegisterListForType("/api/v1/assemblies", server, FoodManager.GetAssemblies);
        RegisterRESTForType("/api/v1/assembly/", server, FoodManager.GetAssembly, FoodManager.CreateAssembly, FoodManager.DeleteAssembly);
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