using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGame.Data;
using VideoGame.Models.Domian;
using VideoGame.Models.DTOs;


namespace VideoGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class VideoGameController(VideoGameDb dbContext) : ControllerBase()
    {
        private readonly VideoGameDb _dbContext = dbContext;
      

      
        [HttpGet("VideoGames")]
        public IActionResult GetAll()
        {
           var  videoGames = _dbContext.VideoGames.ToList();

            var videoGamesDTO = new List<VideoGamesDTO>();

            foreach (var videoGame in videoGames)
            {
                videoGamesDTO.Add(new VideoGamesDTO()
                {
                    Id = videoGame.Id,
                    Title = videoGame.Title,
                    Platform = videoGame.Platform,
                    Developer = videoGame.Developer,
                    Publisher = videoGame.Publisher

                });
            }
            return Ok(videoGamesDTO);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetVideoBtId(Guid id)
        {
            try
            {
                if (id == Guid.Empty) return BadRequest();
                var video = await _dbContext.VideoGames.FirstOrDefaultAsync((x) => x.Id == id);
               
                var videoDTO = new VideoGamesDTO{
                    Id = video.Id,
                    Title = video.Title,
                    Platform = video.Platform,
                    Developer = video.Developer,
                    Publisher = video.Publisher
    };
                return  video== null? NotFound("Video does not exist") :Ok(videoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal error", error = ex.Message });
            }
        }

        [HttpPost]
       public async Task< IActionResult> CreateVideoGame([FromBody] VideoGamesDTO request )
        {
            var  Id = Guid.NewGuid();
            if (request == null) return BadRequest();
            var video = await _dbContext.VideoGames.FirstOrDefaultAsync((x)=>x.Id == Id);
            if (video == null) return NotFound("Database is Empty");
            if (video != null) return BadRequest(new { message = "Video already exist" });

            var data  = new VideoGames()
            {
                Id = Id,
                Title = request.Title,
                Platform = request.Platform,
                Developer = request.Developer,
                Publisher = request.Publisher
            };

            _dbContext.VideoGames.Add(data);

            _dbContext.SaveChanges();

            return Ok("Created Successfully") ;
        }

    }
    
};

//video.Title = request.Title;
//video.Platform = request.Platform;
//video.Title = request.Platform;
//video.Developer = request.Developer;
//video.Publisher = request.Publisher;