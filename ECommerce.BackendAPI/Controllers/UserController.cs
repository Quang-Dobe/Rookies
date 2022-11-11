using AutoMapper;
using ECommerce.BackendAPI.Repository;
using ECommerce.BackendAPI.Service;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.BackendAPI.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(
            IUserRepository userRepository,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }



        [HttpGet]
        [EnableCors("_myAdminSite")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<AllUserDTO>>> GetAllUser()
        {
            try
            {
                List<IdentityUser> identityUsers = await userRepository.GetUsers();
                List<AllUserDTO> allUserDTOs = mapper.Map<List<AllUserDTO>>(identityUsers);
                for (int i = 0; i < identityUsers.Count; i++)
                {
                    IList<string> role = await userManager.GetRolesAsync(identityUsers[i]);
                    allUserDTOs[i].Role = role.ElementAt(0);
                }
                return Ok(allUserDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [EnableCors("_myAdminSite")]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<string>> GetAllRoles()
        {
            try
            {
                IQueryable<IdentityRole> allRoles = roleManager.Roles;
                List<string> roles = new List<string>();
                foreach (IdentityRole role in allRoles)
                {
                    roles.Add(role.Name);
                }
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [EnableCors("_myAdminSite")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateUserRole([FromQuery] string userId, [FromBody] string role)
        {
            try
            {
                IdentityUser user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return BadRequest("Invalid userID");
                }
                var result = roleManager.FindByNameAsync(role).Result;
                if (result == null)
                {
                    return BadRequest("Invalid Role");
                }
                var currentRoles = await userManager.GetRolesAsync(user);
                await userManager.RemoveFromRolesAsync(user, currentRoles);
                await userManager.AddToRoleAsync(user, result.Name);
                return Ok("Update role sucessfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
