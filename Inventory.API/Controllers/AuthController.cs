using Inventory.Application.DTOs;
using Inventory.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory.Application.Services;

namespace Inventory.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
 
 private readonly InventoryDbContext _context;
private readonly JwtService _jwtService;

public AuthController(InventoryDbContext context, JwtService jwtService)
{
    _context = context;
    _jwtService = jwtService;
}

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user == null)
{
    return Unauthorized("Invalid email or password");
}

if (user.Password != request.Password)
{
    return Unauthorized("Invalid email or password");
}

    
    var token = _jwtService.GenerateToken(user);

return Ok(new
{
    Token = token
});
}
}