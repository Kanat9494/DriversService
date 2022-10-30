namespace DriversService.Data;

public class DriverContext : DbContext
{
    public DriverContext(DbContextOptions<DriverContext> options) : base(options)
    { }

    public DbSet<Driver> Drivers { get; set; }
}
