
namespace FoodRush.API.Controllers.v2
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    [AllowAnonymous]
    public class restaurantController : ControllerBase
    {
        private readonly ISender _sender;

        public restaurantController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetTopRatedRestaurants([FromQuery] int count = 10) =>
            Ok(await _sender.Send(new GetTopRatedRestaurantsQuery(count)));
    }
}
