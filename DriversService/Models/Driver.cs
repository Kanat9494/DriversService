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
    public string Itn { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string LicencePlate { get; set; }
    [Required]
    public string BusNumber { get; set; }
}
