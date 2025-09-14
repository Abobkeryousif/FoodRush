namespace FoodRush.Application.Feature.Command.Account
{
    public record LoginUserCommand(LoginUserDto userDto) : IRequest<ApiResponse<string>>;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;
        private readonly ISendEmailService _sendEmail;
        private readonly ILogger<LoginUserCommandHandler> _logger;

        public LoginUserCommandHandler(IUnitofwork unitofwork, ISendEmailService sendEmail, ILogger<LoginUserCommandHandler> logger)
        {
            _unitofwork = unitofwork;
            _sendEmail = sendEmail;
            _logger = logger;
        }

        public async Task<ApiResponse<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Login attempt for Email: {Email}", request.userDto.email);

            var loginUser = await _unitofwork.UserRepository.FirstOrDefaultAsync(u => u.Email == request.userDto.email);
            if (loginUser == null)
            {
                _logger.LogWarning("Login failed - Email not found: {Email}", request.userDto.email);
                return new ApiResponse<string>(HttpStatusCode.NotFound, "Invalid Email Or Password!");
            }

            var verifyPassword = BCrypt.Net.BCrypt.Verify(request.userDto.password, loginUser.Password);
            if (!verifyPassword)
            {
                _logger.LogWarning("Login failed - Invalid password for Email: {Email}", request.userDto.email);
                return new ApiResponse<string>(HttpStatusCode.NotFound, "Invalid Email Or Password!");
            }

            // Generate OTP
            Random random = new Random();
            int otp = random.Next(0, 9999);

            var userOtp = new OTP
            {
                otp = otp.ToString("0000"),
                UserEmail = loginUser.Email,
                ExpirationOn = DateTime.Now.AddMinutes(5),
                IsUsed = false
            };

            await _unitofwork.OtpRepository.CreateAsync(userOtp);

            _sendEmail.SendEmail(loginUser.Email, "Verify 2FA Code", $"Please confirm this code to complete login: {userOtp.otp}");

            _logger.LogInformation("OTP generated and email sent successfully for Email: {Email}", loginUser.Email);

            return new ApiResponse<string>(HttpStatusCode.OK,"Success","We sent a 2FA code to your email. Please confirm it."
            );
        }
    }
}
