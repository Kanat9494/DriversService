using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DriversService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriverController : ControllerBase
{
    private readonly DriverContext _driverContext;

    public DriverController(DriverContext context)
    {
        _driverContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    //[AllowAnonymous]
    //[HttpPost("SignIn")]
    //public async Task<IActionResult> SignIn([FromBody] DriverSignInRequest driver)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        var existingDriver = await Authenticate(driver);
    //        if (existingDriver != null)
    //        {
    //            return Ok(existingDriver);
    //        }
    //    }

    //    return BadRequest(new DriverSignInResponse
    //    {
    //        Errors = new List<string>()
    //        {
    //            "Неправильный логин или пароль!",
    //        }, 
    //        Success = false
    //    });
    //}

    [AllowAnonymous]
    [HttpPost("SignIn")]
    public async Task<DriverResponse?> SignIn([FromBody] DriverSignInRequest driver)
    {
        var signedInDriver = await _driverContext.Drivers.FirstOrDefaultAsync(s =>
                s.PhoneNumber == driver.UserName && s.Password == driver.Password);
        if (ModelState.IsValid && signedInDriver != null)
        {

            return new DriverResponse()
            {
                DriverId = signedInDriver.DriverId,
                UserName = signedInDriver.PhoneNumber,
                BusNumber = signedInDriver.BusNumber
            };
        }

        return null;
    }


    [HttpGet("GetDrivers")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize]
    public async Task<IActionResult> GetDrivers()
    {
        var drivers = await _driverContext.Drivers.ToListAsync();
        return Ok(drivers);
    }

    [HttpGet("GetDriver/{driverId}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetDriver(int driverId)
    {
        var driver = await _driverContext.Drivers.FirstOrDefaultAsync(x => x.DriverId == driverId);

        if (driver == null)
            return NotFound();

        return Ok(driver);
    }

    private async Task<IActionResult?> Authenticate(DriverSignInRequest driver)
    {
        var existingDriver = await _driverContext.Drivers.FirstOrDefaultAsync(x => x.PhoneNumber == driver.UserName
            && x.Password == driver.Password);

        if (existingDriver != null)
        {
            var token = GenerateToken(existingDriver);

            return Ok(new DriverSignInResponse
            {
                Success = true,
                Token = token
            });
        }

        return BadRequest(new DriverSignInResponse
        {
            Errors = new List<string>()
            {
                "Неправильный логин или пароль!",
            },
            Success = false
        });
    }

    private string GenerateToken(Driver? driver)
    {
        var securityKey = AuthOptions.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var identityClaims = new[]
        {
            new Claim("Id", driver.DriverId.ToString()),
            new Claim("PhoneNumber", driver.PhoneNumber),
            new Claim("Itn", driver.Itn),
            new Claim("BusNumber", driver.BusNumber)
        };

        var now = DateTime.UtcNow;

        var token = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: identityClaims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
