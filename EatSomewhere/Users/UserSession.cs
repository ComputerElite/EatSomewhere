using System.Text.Json.Serialization;

namespace EatSomewhere.Users;

public class UserSession
{
    [JsonIgnore]
    public string Id { get; set; }
    public string UserId { get; set; }
    public DateTime LastAccess { get; set; }
    public string? Origin { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ValidUnti { get; set; }
}