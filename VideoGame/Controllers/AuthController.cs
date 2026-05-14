using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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

        [HttpGet("users")]

        public async Task<ActionResult<User>> GetAllUsers()
        {
            try 
            {
                var users= await _dbContext.Users.ToListAsync();

                var userDto = new List<AllUsersDTO>();

                foreach (var user in users)
                {
                    var newAllUsersDTO = new AllUsersDTO
                    {
                        UserName = user.UserName
                    };

                    userDto.Add(newAllUsersDTO);
                }

                Console.WriteLine(users.ToString());
                return Ok(userDto);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new {Message = ex.Message});
            }

        }

        [HttpGet("GetUserById/{id}")]
       public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            try {

                if (id == null) return BadRequest(new { Message = "Field can not be empty" });
                var user = await _dbContext.Users.FirstOrDefaultAsync((x) => x.Id == id);
                if (user == null) return NotFound(new { Message = "User not found" });
                return Ok(user);
            }
            catch (Exception ex)
            {
               return StatusCode(500, new {Message = ex.Message});
            }
        }


        [HttpPost("Register_User")]
      public async Task<ActionResult<User>>  Register(UserDTO request)
        {
            try 
            {
             
                if (request == null) return BadRequest(new {message = "Request can not be empty" });

               var user = await _dbContext.Users.FirstOrDefaultAsync((x)=>x.UserName == request.UserName);
                if (user != null) return BadRequest(new { message = "User already exist" });

                var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);

                var newUser = new User { 
                UserName = request.UserName,
                PasswordHashed = hashedPassword,
                
                };
               

                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();

                return Ok( newUser);
             
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "Internal error", error = ex.Message });
            }

            return Ok();
        }






        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginDTO  request)

        {
            if (request == null) return BadRequest(new { Message = "Request can not be empty" });
            var user = await _dbContext.Users.FirstOrDefaultAsync((x) => x.UserName == request.UserName);
            if (user == null) return NotFound(new { Message = "User not found" });

            var verifyPassword  = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHashed, request.Password); 
            if(verifyPassword == PasswordVerificationResult.Failed ) return BadRequest(new { Message = "Invalid username or password" });

            return Ok(new { Message = "User Successfully logged in" });
        }

    }

}
