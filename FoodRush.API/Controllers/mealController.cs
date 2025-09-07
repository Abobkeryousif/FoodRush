
using FoodRush.Application.Feature.Command.Meals;
using FoodRush.Application.Feature.Query.Meals;

namespace FoodRush.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class mealController : ControllerBase
    {
        private readonly ISender _sender;
        public mealController(ISender sender)=>
            _sender = sender;

        [HttpPost]
        public async Task<IActionResult> CreateMeal(MealDto mealDto) =>
            Ok(await _sender.Send(new CreateMealCommand(mealDto)));

        [HttpGet]
        public async Task<IActionResult> GetAllMeals() =>
            Ok(await _sender.Send(new GetMealsQuery()));

        
    }
}
