
namespace FoodRush.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class accountController : ControllerBase
    {
        private readonly ISender _sender;
        public accountController(ISender sender)=>
            _sender = sender;

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin(LoginUserDto userDto) =>
            Ok(await _sender.Send(new LoginUserCommand(userDto)));
        
    }
}
