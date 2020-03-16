using System.Threading.Tasks;
using JWTAuthorization.Models;
using JWTAuthorization.Repositories;
using JWTAuthorization.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthorization.Controllers
{
    [Route("v1/api/account")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Authenticate([FromBody] User user)
        {
            User userData = UserRepository.GetUser(user.Username, user.Password);

            if (userData == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(userData);
            userData.Password = "";

            return new
            {
                userData,
                token

            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous()
        {
            return "Anonymous";
        }

        [HttpGet]
        [Route("authenticated")]    
        [Authorize]
        public string Authenticated()
        {
            return string.Format("Authenticated - {0}", User.Identity.Name);
        }

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee, manager")]
        public string Employee()
        {
            return "Employees";
        }

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager()
        {
            return "Managers";
        }


    }
}
