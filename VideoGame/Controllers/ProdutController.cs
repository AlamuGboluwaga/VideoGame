using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGame.Data;
using VideoGame.Models.DTOs;
using VideoGame.ProductPagination;

namespace VideoGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(VideoGameDb dbContext) : ControllerBase
    {
        private readonly VideoGameDb _dbContext = dbContext; 

        [HttpGet]
        public async Task<ActionResult<VideoGamesDTO>> GetAllProducts([FromQuery] ProductFilters filter)

        {
            var totalRecords = await _dbContext.Products.CountAsync();
            var products = await _dbContext.Products.Skip((filter.PageNumber -1)* filter.PageSize).Take(filter.PageSize).ToListAsync();
            var paginationResponse = new ProductPaginationResponse( filter.PageNumber, filter.PageSize, totalRecords);

            return Ok( new {Count = totalRecords , PaginationResponse = paginationResponse, Products = products });
        }
    }
}
