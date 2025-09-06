
namespace FoodRush.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    public class restaurantController : ControllerBase
    {
        private readonly ISender _sender;
        public restaurantController(ISender sender)=>
            _sender = sender;

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant(RestaurantDto restaurantDto) =>
            Ok(await _sender.Send(new CreateRestaurantCommand(restaurantDto)));

        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants() =>
            Ok(await _sender.Send(new GetRestaurantsQuery()));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRestaurantById(int id) =>
            Ok(await _sender.Send(new GetByIdRestaurantQuery(id)));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRestaurant(int id,[FromBody] RestaurantDto restaurantDto) =>
            Ok(await _sender.Send(new UpdateRestaurantCommand(id,restaurantDto)));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRestaurant(int id) =>
             Ok(await _sender.Send(new DeleteRestaurantCommand(id)));
    }
}
