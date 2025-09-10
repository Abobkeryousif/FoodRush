
using FoodRush.Application.Contract.Service;

namespace FoodRush.Application.Feature.Command.Account
{
    public record LoginUserCommand(LoginUserDto userDto) : IRequest<ApiResponse<string>>;
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;
        private readonly ISendEmailService _sendEmail;
        public LoginUserCommandHandler(IUnitofwork unitofwork, ISendEmailService sendEmail)
        {
            _unitofwork = unitofwork;
            _sendEmail = sendEmail;
        }

        public async Task<ApiResponse<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var loginUser = await _unitofwork.UserRepository.FirstOrDefaultAsync(u=> u.Email == request.userDto.email);
            if (loginUser == null)
                return new ApiResponse<string>(HttpStatusCode.NotFound,"Invalid Email Or Password!");

            var verifyPassword = BCrypt.Net.BCrypt.Verify(request.userDto.password , loginUser.Password);
            if (!verifyPassword)
                return new ApiResponse<string>(HttpStatusCode.NotFound,"Invalid Email Or Password!");

            //Generate OTP
            Random random = new Random();
            int otp = random.Next(0,9999);

            var userOtp = new OTP
            {
                otp = otp.ToString("0000"),
                UserEmail = loginUser.Email,
                ExpirationOn = DateTime.Now.AddMinutes(5),
                IsUsed = false
            };

            await _unitofwork.OtpRepository.CreateAsync(userOtp);

            _sendEmail.SendEmail(loginUser.Email, "Verifiy 2FA Code",$"Plaese Confirm This Code To Complete Login:  {userOtp.otp}");

            return new ApiResponse<string>(HttpStatusCode.OK,"Success","We Send 2FA Code In Your Email Please Confirm It");
        }
    }
}

