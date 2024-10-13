// File: Controllers/RolesController.cs
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
        [Produces("application/json")]
        public async Task<IActionResult> AddOrReplaceUserRole([FromBody] AddRoleToUserModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return NotFound($"User '{model.Username}' not found.");
            }

            if (!await _roleManager.RoleExistsAsync(model.Role))
            {
                return BadRequest($"Role '{model.Role}' does not exist.");
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Count > 0)
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    return BadRequest("Failed to remove existing roles.");
                }
            }

            var addResult = await _userManager.AddToRoleAsync(user, model.Role);
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


