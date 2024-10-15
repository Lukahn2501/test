using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MovieRanker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    
    public class RolesController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("AddOrReplaceUserRole")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        public async Task<IActionResult> AddOrReplaceUserRole([FromBody] AddRoleToUserModel model)
        {
            IdentityUser? user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return NotFound($"User '{model.Username}' not found.");
            }

            bool roleExists = await _roleManager.RoleExistsAsync(model.Role);
            if (!roleExists)
            {
                return BadRequest($"Role '{model.Role}' does not exist.");
            }

            IList<string> currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Count > 0)
            {
                IdentityResult removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    return BadRequest("Failed to remove existing roles.");
                }
            }

            IdentityResult addResult = await _userManager.AddToRoleAsync(user, model.Role);
            if (addResult.Succeeded)
            {
                return Ok($"Role '{model.Role}' assigned to user '{model.Username}'.");
            }

            return BadRequest(addResult.Errors);
        }
    }

    public class AddRoleToUserModel
    {
        public required string Username { get; set; }
        public required string Role { get; set; }
    }
}