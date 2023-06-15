using Microsoft.AspNetCore.Mvc;
using NoteApp.Api.Services;

namespace NoteApp.Api.Controllers;

[ApiController]
[Route("User")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<string> GetUser(string username)
    {
        return await _userService.GetUserIdAsync(username);
    }
}
