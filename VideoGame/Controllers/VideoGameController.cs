
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using VideoGame.Data;
using VideoGame.Models.Domain;
using VideoGame.Models.DTOs;
using VideoGame.Pagination;


namespace VideoGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class VideoGameController(VideoGameDb dbContext, IHttpClientFactory httpClientFactory) : ControllerBase()
    {
        private readonly VideoGameDb _dbContext = dbContext;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private ReadOnlyMemory<byte> jsonString;


        [HttpGet("VideoGames")]
       public async Task< ActionResult<VideoGamesDTO>> GetAllVideoGames()
        {
            try
            {
                var videoGames = await _dbContext.VideoGames.ToListAsync();

                var videoGamesDTO = new List<VideoGamesDTO>();

                foreach (var videoGame in videoGames)
                {
                    var videoGamesModel = new VideoGamesDTO
                    {
                        
                        Title = videoGame.Title,
                        Platform = videoGame.Platform,
                        Developer = videoGame.Developer,
                        Publisher = videoGame.Publisher

                    };

                    videoGamesDTO.Add(videoGamesModel);
                }
                return Ok(videoGamesDTO);
            }
            catch (Exception ex) {
                return StatusCode(500, new { message = "Internal error", error = ex.Message });
            }
          

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
       public async Task< IActionResult> CreateVideoGame([FromBody] AddVideoGamesDTO request )
        {
            var  Id = Guid.NewGuid();
            if (request == null) return BadRequest();
            var video = await _dbContext.VideoGames.FirstOrDefaultAsync((x)=>x.Id == Id);
            if (video != null) return BadRequest(new { message = "Video already exist" });

            var data  = new VideoGames
            {
                Title = request.Title,
                Platform = request.Platform,
                Developer = request.Developer,
                Publisher = request.Publisher
            };

            _dbContext.VideoGames.Add(data);

            _dbContext.SaveChanges();

            var  response  = new VideoGamesDTO
            {
                Id = data.Id,
                Title = data.Title,
                Platform = data.Platform,
                Developer = data.Developer,
                Publisher = data.Publisher
            };


            return CreatedAtAction( nameof(GetVideoBtId), new { id = data.Id }, response) ;
        }


        [HttpGet("GetAllData")]
        public async Task<IActionResult> GetAllData([FromQuery] PaginationFilter filter)
        {
            // 1. Ensure the filter defaults are respected if none are passed
            filter ??= new PaginationFilter();

            var httpClient = _httpClientFactory.CreateClient();

            // 2. Inject the filter variables into the DataCite URL dynamically
            var url = $"https://api.test.datacite.org/providers/caltech/dois?page[number]={filter.PageNumber}&page[size]={filter.PageSize}";

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Failed to fetch data from the external provider.");
            }

            // Fixed: Named it jsonString so the JSON parser can find it
            var jsonString = await response.Content.ReadAsStringAsync();

            // 3. Extract the total records from DataCite's response metadata
            int totalRecords = 0;
            using (JsonDocument doc = JsonDocument.Parse(jsonString))
            {
                if (doc.RootElement.TryGetProperty("meta", out var meta) &&
                    meta.TryGetProperty("total", out var total))
                {
                    totalRecords = total.GetInt32();
                }
            }

            // 4. Instantiated PaginationResponse inside the method using the fetched total records
            var paginationResponse = new PaginationResponse(filter.PageNumber, filter.PageSize, totalRecords);

            // 5. Return both the metadata and the actual payload
            return Ok(new
            {
                Pagination = paginationResponse,
                //Data = JsonDocument.Parse(jsonString).RootElement.GetProperty("data")
            });
        }



    }

};

