using System.Text.Json;

namespace EatSomewhere;


public class Config
{

    public static Config? Instance;

    public string? dbConnectionString { get; set; } =
        "Server=127.0.0.1;Port=5432;Database=eatsomewhere;Username=eatsomewhere;Password=eatsomewhere;";
    public int port { get; set; } = 8383;
    private static readonly string _configPath = "config.json";
    public static void LoadConfig()
    {
        if(!File.Exists(_configPath))
        {
            Instance = new Config();
            SaveConfig();
            return;
        }
        Instance = JsonSerializer.Deserialize<Config>(File.ReadAllText(_configPath));
    }

    public static void SaveConfig()
    {
        File.WriteAllText(_configPath, JsonSerializer.Serialize(Instance));
    }
}