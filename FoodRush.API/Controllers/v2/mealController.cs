
namespace FoodRush.API.Controllers.v2
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiVersion("2.0")]
    public class mealController : ControllerBase
    {
        private readonly ISender _sender;

        public mealController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMeal([FromQuery]QueryParameters parameters) =>
            Ok(await _sender.Send(new GetMealsQueryV2(parameters)));

    }
}
