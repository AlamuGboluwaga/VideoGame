 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGame.Data;


namespace VideoGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Instant_SavingsController(VideoGameDb dbContext) : ControllerBase
    {
        private readonly VideoGameDb _dbContext = dbContext;

        [HttpGet]
        public async Task<ActionResult> InstantAccount()
        {
            var data = await _dbContext.InstantSavings.ToListAsync();

            return Ok(data);
        }



    }
}
