
namespace FoodRush.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class orderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public orderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = "USER,ADMIN,SUPERADMIN")]
        [ValidateModel]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = GetUserEmail();
            if (string.IsNullOrEmpty(buyerEmail))
                return BadRequest("Not Found Email!");

            var order = await _orderService.CreateOrderAsync(orderDto,buyerEmail);
            return Ok(order);            
        }

        [HttpGet]
        [Authorize(Roles = "USER,ADMIN,SUPERADMIN")]
        [ValidateModel]
        public async Task<IActionResult> GetOrderForUser()
        {
            var email = GetUserEmail();

            if (string.IsNullOrEmpty(email))
                return BadRequest("Not Found Email!");

            var order = await _orderService.GetAllUserOrdersAsync(email);
            return Ok(order);

        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "USER,ADMIN,SUPERADMIN")]
        [ValidateModel]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var email = GetUserEmail();

            if (string.IsNullOrEmpty(email))
                return BadRequest("Not Found Email!");

            return Ok(await _orderService.GetOrderByIdAsync(id, email)); 
        }

        [HttpGet("get-delivery")]
        public async Task<IActionResult> GetDeliveryMethod() =>
            Ok(await _orderService.GetAllDeliveryMethodAsync());
            






        private string? GetUserEmail()
        {
            return User.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
