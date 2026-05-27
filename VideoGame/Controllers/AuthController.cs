//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualBasic;
//using VideoGame.Data;
//using VideoGame.Models.Domain;
//using VideoGame.Models.DTOs;
//using VideoGame.Validator;
//using FluentValidation;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
//using VideoGame;


//namespace VideoGame.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController(VideoGameDb dbContext, IValidator<UserDTO> validator) : ControllerBase

//    {
//        private readonly VideoGameDb _dbContext = dbContext;
//        private readonly IValidator<UserDTO> _validator = validator;

//        [HttpGet("users")]

//        public async Task<ActionResult<User>> GetAllUsers()
//        {
//            try 
//            {
//              var skipNumber = (query.PageNuber - 1) * query.PageSize;

//                var users= await _dbContext.Users.Skip(skipNumber).Take().ToListAsync();

//                var userDto = new List<AllUsersDTO>();

//                foreach (var user in users)
//                {
//                    var newAllUsersDTO = new AllUsersDTO
//                    {
//                        UserName = user.UserName
//                    };

//                    userDto.Add(newAllUsersDTO);
//                }

//                Console.WriteLine(users.ToString());

             


//                return Ok(userDto);
//            }
//            catch(Exception ex)
//            {
//                return StatusCode(500, new {Message = ex.Message});
//            }

//        }

//        [HttpGet("GetUserById/{id}")]
//       public async Task<ActionResult<User>> GetUserById(Guid id)
//        {
//            try {

//                if (id == null) return BadRequest(new { Message = "Field can not be empty" });
//                var user = await _dbContext.Users.FirstOrDefaultAsync((x) => x.Id == id);
//                if (user == null) return NotFound(new { Message = "User not found" });
//                return Ok(user);
//            }
//            catch (Exception ex)
//            {
//               return StatusCode(500, new {Message = ex.Message});
//            }
//        }


//        [HttpPost("Register_User")]
//      public async Task<ActionResult<User>>  RegisterUser([FromBody] UserDTO request)
//        {
//            try
//            {
//                if (request == null ) return BadRequest(new { message = "Request can not be empty" });

//                var validationResult = await _validator.ValidateAsync(request);
//                if (!validationResult.IsValid)
//                {
//                    return BadRequest(validationResult.ToDictionary());
//                }

//                var user = await _dbContext.Users.FirstOrDefaultAsync((x) => x.UserName == request.UserName);
//                if (user != null) return BadRequest(new { message = "User already exist" });
//                var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
//                var newUser = new User {
//                    UserName = request.UserName,
//                    PasswordHashed = hashedPassword,
//                };
//                _dbContext.Users.Add(newUser);
//                await _dbContext.SaveChangesAsync();

//                return CreatedAtAction(nameof(GetUserById), new {id= newUser.Id}, newUser);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = "Internal error", error = ex.Message });
//                    }
//        }

//        [HttpPost("Login")]
//        public async Task<ActionResult<User>> Login([FromBody] UserDTO request)

//        {
//            try {
               
//                if (request == null) return BadRequest(new { Message = "Request can not be empty" });
//                var verificationResult = await _validator.ValidateAsync(request);
//                if (!verificationResult.IsValid)
//                {
//                    return BadRequest(verificationResult.ToDictionary());
//                }

//                var user = await _dbContext.Users.FirstOrDefaultAsync((x) => x.UserName == request.UserName);
//                if (user == null) return NotFound(new { Message = "Invalid username or password" });

//                var verifyPassword = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHashed, request.Password);
//                if (verifyPassword == PasswordVerificationResult.Failed) return BadRequest(new { Message = "Invalid username or password" });

//                return Ok(new { Message = "User successfully logged in" });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = "Internal error", error = ex.Message });
//            }   
//        }


//        [HttpPut("PasswordReset")]
//       public async Task<ActionResult<User>> PasswordReset([FromBody] ResetPassword request)
//        {
//            try { 
//            if(request == null) return BadRequest(new { message = "Request can not be empty" });
//                var user = await _dbContext.Users.FirstOrDefaultAsync((x) => x.UserName == request.UserName);
//                if (user == null) return NotFound(new { message = "User not found" });

//                if (request.PasswordHashed != request.ConfirmPassword) return BadRequest(new { message = "Password and confirm password do not match" });

//                    var passwordHasher = new PasswordHasher<User>().HashPassword(user, user.PasswordHashed);
//                    var resetPassword = new User
//                    {
//                        PasswordHashed = passwordHasher
//                    };
//                    _dbContext.Users.Add(resetPassword);
//                    _dbContext.SaveChanges();
              
//                return Ok(new { message = "Password reset successful" });

//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = "Internal error", error = ex.Message });
//            }

           
//        }

//        [HttpDelete("DeleteUser/{id}")]
//        public async Task<ActionResult<User>> DeleUser(Guid id)
//        {
//            try
//            {
//                if (id == null) return BadRequest(new { Message = "id can not be empty" });
//                var user = await _dbContext.Users.FirstOrDefaultAsync((x) => x.Id == id);
//                if (user == null) return NotFound(new { Message = "User not found" });

//                _dbContext.Users.Remove(user);
//                _dbContext.SaveChanges();

//                return Ok("User successfully deleted");
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { Message = ex.Message });
//            }
//        }

//    }
//}
