
namespace FoodRush.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class basketController : ControllerBase
    {
        private readonly ISender _sender;
        public basketController(ISender sender)=>
            _sender = sender;

        [HttpPost]
        public async Task<IActionResult> CreateBasket(Basket basket) =>
            Ok(await _sender.Send(new CreateBasketCommand(basket)));

        [HttpGet]
        public async Task<IActionResult> GetAllBaskets() =>
            Ok(await _sender.Send(new GetBasketsQuery()));

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetBasketById(string id) =>
            Ok(await _sender.Send(new GetByIdBasketQuery(id)));

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket([FromQuery] string id) =>
            Ok(await _sender.Send(new DeleteBasketCommand(id)));
        
    }
}
