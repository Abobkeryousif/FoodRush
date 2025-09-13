
namespace FoodRush.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "USER,ADMIN,SUPERADMIN")]
    public class paymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public paymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(string basketId,int deliveryId)
        {
            var payment = await _paymentService.CreateOrUpdatePaymentAsync(basketId, deliveryId);
            return Ok(payment);
        }
    }
}
