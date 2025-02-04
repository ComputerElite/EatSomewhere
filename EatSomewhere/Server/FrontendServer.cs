using System.Text.Json;
using ComputerUtils.Logging;
using ComputerUtils.Webserver;

namespace EatSomewhere.Server;

public class FrontendServer
{
    public static void AddFrontendRoutes(HttpServer server)
    {
        // Add routes for frontend
        AddDirectory(server, "frontend", "");
        server.AddRoute("GET", "/api/v1/alive", request =>
        {
            request.SendString(JsonSerializer.Serialize(new ApiResponse {Success = true}), "application/json");
            return true;
        });
    }

    public static void AddDirectory(HttpServer server, string dirPath, string serverPath)
    {
        if(!Directory.Exists(dirPath)) return;
        foreach (string file in Directory.GetFiles(dirPath))
        {
            string fileName = Path.GetFileName(file);
            if(fileName == "index.html") fileName = "";
            if (fileName.EndsWith(".html"))
            {
                string route = serverPath + "/" + Path.GetFileNameWithoutExtension(fileName);
                server.AddRouteFile(route, file, true, false);
                server.AddRouteRedirect("GET", route + "/", route, false, false, false);
            }
            server.AddRouteFile(serverPath + "/" + fileName, file);
        }
        foreach (string dir in Directory.GetDirectories(dirPath))
        {
            AddDirectory(server, dir, serverPath + "/" + Path.GetFileName(dir));
        }
    }
}