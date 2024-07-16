using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Store.Application.Features.Account.DTOs;
using Store.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }


        [HttpPost("Resgister")]
        public async Task<ActionResult> Resgister(UserResgisterDTO newUser)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await userManager.FindByEmailAsync(newUser.Email);
                if (existingUser != null)
                {
                    return BadRequest("Email address already exists");
                }

                var user = new ApplicationUser
                {
                    Email = newUser.Email,
                    UserName = newUser.UserName
                };

                var result = await userManager.CreateAsync(user, newUser.Password);
                if (result.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(user, "USER");
                    if (roleResult.Succeeded)
                    {
                        return Ok("Account created successfully and User role assigned");
                    }
                    else
                    {
                        return BadRequest(roleResult.Errors);
                    }
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            return BadRequest(ModelState);
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDTO loginUser)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginUser.Email);
                if (user != null)
                {
                    var isPasswordValid = await userManager.CheckPasswordAsync(user, loginUser.Password);
                    if (isPasswordValid)
                    {
                        var roles = await userManager.GetRolesAsync(user);
                        var token = GenerateJwtToken(user, roles);
                        return Ok(token);
                    }
                }

                return BadRequest("Invalid email or password.");
            }

            return BadRequest(ModelState);
        }

        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecritKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenOptions = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIss"],
                audience: configuration["JWT:ValidAud"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }




        [HttpPost("AddAdmin")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> AddAdmin(UserResgisterDTO createAdminDTO)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await userManager.FindByEmailAsync(createAdminDTO.Email);
                if (existingUser != null)
                {
                    return BadRequest("Email address already exists");
                }

                var roleExists = await roleManager.RoleExistsAsync("ADMIN");
                if (!roleExists)
                {
                    return BadRequest("Role Admin doesn't exist");
                }

                var user = new ApplicationUser
                {
                    Email = createAdminDTO.Email,
                    UserName = createAdminDTO.UserName
                };

                var result = await userManager.CreateAsync(user, createAdminDTO.Password);
                if (result.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(user, "ADMIN");
                    if (roleResult.Succeeded)
                    {
                        return Ok("Admin created successfully");
                    }
                    else
                    {
                        return BadRequest(roleResult.Errors);
                    }
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


    }
}
