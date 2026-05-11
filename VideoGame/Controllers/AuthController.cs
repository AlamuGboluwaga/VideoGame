using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VideoGame.Models.Domain;
using VideoGame.Models.DTOs;

namespace VideoGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new();

        [HttpPost]
      public ActionResult<User> Register(UserDTO request)
        {
            var hashedPassword = new PasswordHasher<User>().HashPassword(user,request.Password); 

            user.UserName = request.UserName;   
            user.PasswordHashed = hashedPassword;
            return Ok(user);
        }

    }
}
