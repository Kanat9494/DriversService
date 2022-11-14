namespace DriversService.Models.DTOs.Responses;

public class DriverResponse
{
    public int DriverId { get; set; }
    public string UserName { get; set; } = null!;
    public string BusNumber { get; set; } = null!;
}
