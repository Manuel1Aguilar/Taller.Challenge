using challenge.Core.Dtos;
using challenge.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace challenge.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(ILogger<UserController> logger, IUserService userService) : ControllerBase
{
    private readonly ILogger<UserController> _logger = logger;
    private readonly IUserService _userService = userService;

    [HttpGet(Name = "GetByUsername")]
    public async Task<ActionResult<UserDto>> GetByUsername(string username)
    {
        _logger.LogInformation("Getting user by username: {username}", username);
        var userResult = await _userService.GetUserAsync(username);
        if (userResult.IsSuccess)
        {
            return Ok(userResult.Value);
        }
        else
        {
            return BadRequest(userResult.Error);
        }
    }
}
