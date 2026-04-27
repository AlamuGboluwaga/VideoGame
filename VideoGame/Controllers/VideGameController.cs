using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VideoGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideGameController : ControllerBase
    {
       private static  List<VideoGame> videoGames = new List<VideoGame>
        {
            new VideoGame { Id = 1, Title = "The Legend of Zelda: Breath of the Wild", Platform = "Nintendo Switch", Developer = "Nintendo", Publisher = "Nintendo" },
            new VideoGame { Id = 2, Title = "God of War", Platform = "PlayStation 4", Developer = "Santa Monica Studio", Publisher = "Sony Interactive Entertainment" },
            new VideoGame { Id = 3, Title = "Red Dead Redemption 2", Platform = "PlayStation 4, Xbox One, PC", Developer = "Rockstar Games", Publisher = "Rockstar Games" }
        };

        [HttpGet("GetVideoGames")]

      public ActionResult<List<VideoGame> > GetVideoGames()
        {
            if(videoGames == null || videoGames.Count == 0)
            {
                return NotFound("No video games found.");
            }

            return Ok(videoGames);

        }

        [HttpGet("GetVideoGameById")]

        public ActionResult<List<VideoGame>> GetVideoGameById(int id)
        {
            var idExist = videoGames.FirstOrDefault((x)=>(x.Id == id));

            if(idExist == null) return NotFound(new { status =404 ,message= $"Video game with id number {id} was not found" });

         return Ok(idExist);
        }

        [HttpPut("UpdateVideoGame")]

      public ActionResult <VideoGame>  UpdateVideoGame(int id)
        {
            var video = videoGames.FirstOrDefault((x) => (x.Id == id));
            if(video == null) return NotFound(new { status = 404, message = $"Video game with id number {id} was not found" });

            return Ok();
        }



    }
}
