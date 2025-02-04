using ComputerUtils.Webserver;

namespace EatSomewhere.Server;

public class Webserver
{
    public HttpServer Server = new ();

    public void SetupRoutesAndStartServer()
    {
        Server.defaultResponseHeaders.Add("Access-Control-Allow-Credentials", "true");
        Server.autoServeOptions = true;
        Server.MaxWebsocketMessageSize = 1024 * 1024 * 5;
        UserManagementServer.AddUsermanagementEndpoints(Server);
        FrontendServer.AddFrontendRoutes(Server);
        FoodWebserver.AddFoodRoutes(Server);
        Server.StartServer(Config.Instance.port);
    }
}