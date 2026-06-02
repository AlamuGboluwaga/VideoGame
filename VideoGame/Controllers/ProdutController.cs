using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGame.Data;
using VideoGame.Models.DTOs;

namespace VideoGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutController(VideoGameDb dbContext) : ControllerBase
    {
        private readonly VideoGameDb _dbContext = dbContext; 

        [HttpGet]
        public async Task<ActionResult<VideoGamesDTO>> GetAllProducts()

        {
            var products = await _dbContext.Products.ToListAsync();
            return Ok(products);
        }
    }
}
