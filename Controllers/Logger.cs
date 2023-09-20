using learnprogramming.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace learnprogramming.Controllers
{
  
    [Route ("api/[controller]")]
    [ApiController]
    public class Logger : ControllerBase
    {
        private readonly IConfiguration iconfig;

        public Logger (IConfiguration iconfig)
        {
            this.iconfig = iconfig;
        }   

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Mlogin logger)
        {          
            if ((string.IsNullOrEmpty(logger.User)) || string.IsNullOrEmpty(logger.Password))
            {
                logger.error = "credenciales incorrectas";
                return BadRequest(logger);
            }

            if ((logger.User != "usertest") && (logger.Password != "123prueba"))
            {

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(iconfig["JWT:Secret"]));
                int expire = Convert.ToInt32(iconfig["JWT:ExpireHours"]);

                var autclain = new List<Claim>
            {
                new Claim(ClaimTypes.Name , logger.User),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
                var token = new JwtSecurityToken(
                        issuer: iconfig["JWT:ValidIssuer"],
                        audience: iconfig["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(expire),
                        claims: autclain,
                        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                        );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo.ToLocalTime()
                });
            }
            else {
                return BadRequest("UNAUTHORIZED");
            }
        }
    }
}
