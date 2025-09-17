
namespace FoodRush.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    public class mealController : ControllerBase
    {
        private readonly ISender _sender;
        public mealController(ISender sender)=>
            _sender = sender;

        [HttpPost]
        [Authorize(Roles = "ADMIN,SUPERADMIN")]
        public async Task<IActionResult> CreateMeal(AddMealDto mealDto) =>
            Ok(await _sender.Send(new CreateMealCommand(mealDto)));

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMeals() =>
            Ok(await _sender.Send(new GetMealsQuery()));

        [HttpGet("{id:int}")]
        [Authorize(Roles = "ADMIN,SUPERADMIN")]
        public async Task<IActionResult> GetMealById(int id) =>
            Ok(await _sender.Send(new GetMealByIdQuery(id)));

        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMIN,SUPERADMIN")]
        public async Task<IActionResult> UpdateMeal(int id, updateMealDto mealDto) =>
            Ok(await _sender.Send(new UpdateMealCommand(id,mealDto)));

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "ADMIN,SUPERADMIN")]
        public async Task<IActionResult> PatchMealAvailability(int id,PatchMealAvailabilityDto patchMeal) =>
            Ok(await _sender.Send(new PatchMealAvailabilityCommand(id,patchMeal)));

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMIN,SUPERADMIN")]
        public async Task<IActionResult> DeleteMeal(int id) =>
            Ok(await _sender.Send(new DeleteMealCommand(id)));

    }
}
