namespace DriversService.Configuration;

public class AuthResult
{
    public bool Success { get; set; }
    public string Token { get; set; }
    public List<string> Errors { get; set; }
}
