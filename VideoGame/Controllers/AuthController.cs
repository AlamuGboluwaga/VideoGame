using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGame.Data;
using VideoGame.Models.Domain;
using VideoGame.Models.DTOs;

namespace VideoGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(VideoGameDb dbContext) : ControllerBase

    {
        private readonly VideoGameDb _dbContext = dbContext;

        //public static User user = new();

        [HttpPost("Register")]
      public async Task<ActionResult<User>>  Register(UserDTO request)
        {
            try 
            {
                if (request == null) return BadRequest(new {message = "Request can not be empty" });
               var user = await _dbContext.Users.FirstOrDefaultAsync((x)=>x.UserName == request.UserName);
                if (user != null) return BadRequest(new { message = "User already exist" });
                var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);

                user.UserName = request.UserName;
                user.PasswordHashed = hashedPassword;
             
            }
            catch(Exception ex)
            {

            }

            return Ok();
        }

    }


    //[HttpPost("login")]
    //public ActionResult<User> Login([FromBody] UserDTO reques)
    //{
    //    //var verify = new PasswordHasher<User>().VerifyHashedPassword(user); 
    //    return Ok(new {message = "Login Successfull"});
    //}
}
