using Microsoft.AspNetCore.Mvc;

namespace ClaretBrain.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IConfiguration cfg) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] TokenRequest req)
    {
        var expected = cfg["Auth:Token"];
        if (string.IsNullOrWhiteSpace(expected)) return Ok(new { ok = true });
        if (req.Token == expected) return Ok(new { ok = true });
        return Unauthorized(new { ok = false });
    }
}

public record TokenRequest(string Token);
