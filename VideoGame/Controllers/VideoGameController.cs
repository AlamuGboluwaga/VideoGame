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

                return Ok(video);
            }
            catch(Exception)
            {
                return NoContent(Exception);
            }
        }




        [HttpPost]
       public IActionResult CreateVideoGame([FromBody] VideoGames request )
        {
            request.Id = Guid.NewGuid();
            //if (request == null) return BadRequest();
            var video = _dbContext.VideoGames.FirstOrDefault((x)=>x.Id == request.Id);
            //if (video == null) return NotFound("Database is Empty");
            if (video != null) return BadRequest(new {message = "Video already exist"});

            _dbContext.VideoGames.Add(request);

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