using System.Text.Json;
using ComputerUtils.Webserver;
using EatSomewhere.Data;
using EatSomewhere.Manager;
using EatSomewhere.Users;

namespace EatSomewhere.Server;

public class FoodWebserver
{
    public static void AddFoodRoutes(HttpServer server)
    {
        ////// ASSEMBLIES //////
        server.AddRoute("GET", "/api/v1/assemblies", request =>
        {
            request.allowAllOrigins = true;
            User? user = UserManagementServer.GetUserBySession(request);
            if (user == null)
            {
                ApiError.SendUnauthorized(request);
                return true;
            }
            request.SendString(JsonSerializer.Serialize(FoodManager.GetAssemblies(user)), "application/json");
            return true;
        });
        server.AddRoute("GET", "/api/v1/assembly/", request =>
        {
            request.allowAllOrigins = true;
            User? user = UserManagementServer.GetUserBySession(request);
            if (user == null)
            {
                ApiError.SendUnauthorized(request);
                return true;
            }

            string id = request.pathDiff;
            request.SendString(JsonSerializer.Serialize(FoodManager.GetAssembly(user, id)), "application/json");
            return true;
        });
        server.AddRoute("POST", "/api/v1/assembly/", request =>
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
                if(assembly == null) throw new Exception();
            } catch
            {
                ApiError.MalformedRequest(request);
                return true;
            }
            request.SendString(JsonSerializer.Serialize(FoodManager.CreateAssembly(user, assembly)), "application/json");
            return true;
        });
        server.AddRoute("DELETE", "/api/v1/assembly/", request =>
        {
            request.allowAllOrigins = true;
            User? user = UserManagementServer.GetUserBySession(request);
            if (user == null)
            {
                ApiError.SendUnauthorized(request);
                return true;
            }
            string id = request.pathDiff;
            request.SendString(JsonSerializer.Serialize(FoodManager.DeleteAssembly(user, id)), "application/json");
            return true;
        });
    }
}