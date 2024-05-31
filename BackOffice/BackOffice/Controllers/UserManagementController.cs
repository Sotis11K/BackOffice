using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BackOffice.Data;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("GetUsers")]
    public IActionResult GetUsers()
    {
        var users = _userManager.Users.ToList();
        return Ok(users);
    }

    [HttpPost("http://localhost:7024/api/Users/{user.Id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] ApplicationUser updatedUser)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        user.FirstName = updatedUser.FirstName;
        user.LastName = updatedUser.LastName;
        user.Email = updatedUser.Email;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded) return NoContent();

        return BadRequest(result.Errors);
    }

    [HttpDelete("DeleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded) return NoContent();

        return BadRequest(result.Errors);
    }
}
