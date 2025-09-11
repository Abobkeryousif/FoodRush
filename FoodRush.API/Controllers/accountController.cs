
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

        [HttpPost("resend-otp")]
        [AllowAnonymous]
        public async Task<IActionResult> ResendOtp(ResendOtpInUserEmail email) =>
            Ok(await _sender.Send(new ResendOtpCommand(email)));

        [HttpPost("forget-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto forgetPasswordDto) =>
            Ok(await _sender.Send(new ForgetPasswordCommand(forgetPasswordDto)));

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto) =>
            Ok(await _sender.Send(new ResetPasswordCommand(resetPasswordDto)));
        
    }
}
