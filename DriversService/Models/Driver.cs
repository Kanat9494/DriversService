namespace DriversService.Models;

public class Driver
{
    [Key]
    public int DriverId { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string FullName { get; set; }
    [Required]
    public string ITIN { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string LicencePlate { get; set; }
    [Required]
    public string BusNumber { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public byte SignedIn { get; set; }
}
