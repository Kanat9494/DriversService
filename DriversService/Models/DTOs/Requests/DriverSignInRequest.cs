namespace DriversService.Models.DTOs.Requests;

public class DriverSignInRequest
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
