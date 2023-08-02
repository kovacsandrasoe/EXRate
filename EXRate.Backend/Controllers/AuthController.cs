using EXRate.Backend.Models;
using EXRate.Backend.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace EXRate.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPut]
        public async Task<IActionResult> InsertUser([FromBody] RegisterViewModel model)
        {
            try
            {
                var users = _userManager.Users.ToList();

                var user = new AppUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    Errorinfo ei = new Errorinfo("");
                    foreach (var item in result.Errors)
                    {
                        ei.Message += item.Description + ",";
                    }
                    ei.Message = ei.Message.Substring(0, ei.Message.Length - 1);
                    return BadRequest(ei);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new Errorinfo(ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (!user.EmailConfirmed)
                {
                    return Unauthorized();
                }
                var claim = new List<Claim>();
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    claim.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
                    claim.Add(new Claim(JwtRegisteredClaimNames.Name, user.Email));
                    claim.Add(new Claim(JwtRegisteredClaimNames.NameId, user.Id));
                    claim.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
                    claim.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    claim.Add(new Claim("id", user.Id));

                    foreach (var role in await _userManager.GetRolesAsync(user))
                    {
                        claim.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var signinKey = new SymmetricSecurityKey(
                      Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

                    int expiryInMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

                    var token = new JwtSecurityToken(
                      issuer: _configuration["Jwt:Site"],
                      audience: _configuration["Jwt:Site"],
                      claims: claim.ToArray(),
                      expires: DateTime.Now.AddMinutes(60),
                      signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(
                      new
                      {
                          token = new JwtSecurityTokenHandler().WriteToken(token),
                          expiration = token.ValidTo
                      });
                };
                return Unauthorized();


            }
            catch (Exception ex)
            {
                return BadRequest(new Errorinfo(ex.Message));
            }
        }
    }
    }
