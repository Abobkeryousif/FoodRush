
namespace FoodRush.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    public class accountController : ControllerBase
    {
        private readonly ISender _sender;
        public accountController(ISender sender)=>
            _sender = sender;

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> UserLogin(LoginUserDto userDto) =>
            Ok(await _sender.Send(new LoginUserCommand(userDto)));

        [HttpPost("complete-login")]
        [AllowAnonymous]        
        public async Task<IActionResult> CompleteLogin(UserOtpDto userOtp) =>
            Ok(await _sender.Send(new CompleteLoginCommand(userOtp)));
        
    }
}
