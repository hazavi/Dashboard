using Microsoft.AspNetCore.Mvc;
using NewsAPI.Services;
using System.Threading.Tasks;

namespace NewsAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("top-news")]
        public async Task<IActionResult> GetTopNews()
        {
            try
            {
                var newsResponse = await _newsService.GetTopNewsAsync();
                return Ok(newsResponse.TopNews[0].News);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
    

}
