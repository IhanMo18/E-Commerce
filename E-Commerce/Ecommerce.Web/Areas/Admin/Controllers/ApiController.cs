using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Areas.Admin.Controllers;

public class ApiController : Controller
{
    [HttpGet("/users/img/{userId}")]
    public async Task<IActionResult> GetProfilePhoto(string userId, [FromServices] UserManager<User> userManager)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();

        var imgUrl = user.ImgProfile ?? "~/img/useer2.jpg";

        return Ok(new { url = imgUrl });
    }
    
    
    [HttpGet("/users/username/{userId}")]
    public async Task<IActionResult> GetUsername(string userId, [FromServices] UserManager<User> userManager)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();

        var username = user.UserName ?? "User";

        return Ok(new { username = username });
    }
}