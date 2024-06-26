using CrudApi.Models.Domain;
using CrudApi.Models.Domain.Authentication.Login;
using CrudApi.Models.Domain.Authentication.SignUp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser>  _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticationController(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegsisterUser regsisterUser, string role)
        {
            // Check if user Exists
            var userExists = await _userManager.FindByEmailAsync(regsisterUser.Email);

            if(userExists != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message="User Already Exists !"});
            }

            //Add the user in the Database
            IdentityUser user = new()
            {
                Email = regsisterUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = regsisterUser.Username,
            };
           

            if(await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(user, regsisterUser.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                      new Response { Status = "Error", Message = "User Failed to Create!" }); ;
                }
                // Add Role to the user
                await _userManager.AddToRoleAsync(user, role);
                return StatusCode(StatusCodes.Status200OK,
                     new Response { Status = "Success", Message = "User Created Successfully!" }); ;

            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     new Response { Status = "Error", Message = "This Role doesn't exist!" });
            }
           
            // Assign Role
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            //checking the user
            var user = await _userManager.FindByNameAsync(loginModel.Username);

            if(user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {

                //claimlist creation
                var authClaims = new List<Claim>
                 {
                     new Claim(ClaimTypes.Name, user.UserName),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                 };

                //we add roles to the list
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                
                // generate the token with the claims
                var jwtToken = GetToken(authClaims);

                //returning the token
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
            }
            return Unauthorized();
           
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey,SecurityAlgorithms.HmacSha256));
            return token;

        }
    }
}
