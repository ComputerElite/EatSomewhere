namespace EatSomewhere.Users;
public class LoginRequest 
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class LoginResponse
{
    public bool? Requires2fa { get; set; }
    public string? Nonce { get; set; }
    public string? SessionId { get; set; }
    public string? Error { get; set; }
    public bool Success { get; set; } = false;
    public string? ChallengeId { get; set; }
}

public class Challenge
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string Nonce { get; set; }
    public ChallengeType Type { get; set; }
    public string Username { get; set; }
}

public enum ChallengeType
{
    Password,
    TOTP,
    Register
}