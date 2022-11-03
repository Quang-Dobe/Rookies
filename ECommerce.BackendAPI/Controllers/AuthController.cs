﻿using AutoMapper;
using ECommerce.BackendAPI.Repository;
using ECommerce.SharedView.DTO.Account;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SharedView.DTO.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.BackendAPI.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration config;
        private readonly IUserRepository userRepository;
        private readonly ICartRepository cartRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(IConfiguration config, 
            IUserRepository userRepository, 
            ICartRepository cartRepository,
            IOrderRepository orderRepository,
            IMapper mapper, 
            SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager)
        {
            this.config = config;
            this.userRepository = userRepository;
            this.cartRepository = cartRepository;
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginRequestDTO loginRequestModel)
        {
            // Get request successfully
            var result = await signInManager.PasswordSignInAsync(loginRequestModel.UserName, loginRequestModel.Password, false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var issuer = config["Jwt:Issuer"];
                var audience = config["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes
                (config["Jwt:Key"]);
                // Create new token-hash-tool to hash issuer, audience, key
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, loginRequestModel.UserName),
                new Claim(JwtRegisteredClaimNames.Email, loginRequestModel.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };

                IdentityUser identityUser = await userRepository.GetUserByName(loginRequestModel.UserName);
                Console.WriteLine("--" + identityUser.UserName);
                var roles = await userManager.GetRolesAsync(identityUser);

                var claims = new[] {
                    new Claim(ClaimTypes.Name, identityUser.UserName),
                    new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
                };
                foreach (var item in claims)
                {
                    tokenDescriptor.Subject.AddClaim(item);
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);

                return Ok(stringToken);
            }

            return Unauthorized("Invalid account");
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RegisterRequestDTO registerRequestModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = mapper.Map<IdentityUser>(registerRequestModel);
                var createUserResult = await userManager.CreateAsync(user, registerRequestModel.Password);

                if (createUserResult.Succeeded)
                {
                    // If user register sucessfully, create for him/her an empty cart to store what they like to buy
                    await cartRepository.CreateCart(user.Id);
                    await orderRepository.CreateOrder(user.Id);

                    await cartRepository.Save();
                    await orderRepository.Save();
                    return Ok("Create sucessfully!");
                }
                else
                {
                    return StatusCode(401, createUserResult.Errors);
                }
            }

            return BadRequest(ModelState.Values.SelectMany(x => x.Errors));
        }


        [HttpGet]
        public async Task<ActionResult<List<AllUserDTO>>> GetAllUser()
        {
            try
            {
                List<IdentityUser> identityUsers = await userRepository.GetUsers();
                List<AllUserDTO> allUserDTOs = mapper.Map<List<AllUserDTO>>(identityUsers);
                return Ok(allUserDTOs);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
