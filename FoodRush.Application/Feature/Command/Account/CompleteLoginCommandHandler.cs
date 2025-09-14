
namespace FoodRush.Application.Feature.Command.Account
{
    public record CompleteLoginCommand(UserOtpDto otpDto) : IRequest<ApiResponse<string>>;

    public class CompleteLoginCommandHandler : IRequestHandler<CompleteLoginCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;
        private readonly ITokenSerivce _tokenSerivce;
        private readonly ILogger<CompleteLoginCommandHandler> _logger;

        public CompleteLoginCommandHandler(IUnitofwork unitofwork, ITokenSerivce tokenSerivce, ILogger<CompleteLoginCommandHandler> logger)
        {
            _unitofwork = unitofwork;
            _tokenSerivce = tokenSerivce;
            _logger = logger;
        }

        public async Task<ApiResponse<string>> Handle(CompleteLoginCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Completing login for OTP: {OTP}", request.otpDto?.otp);

            if (request.otpDto == null || string.IsNullOrEmpty(request.otpDto.otp))
            {
                _logger.LogWarning("OTP is missing in request");
                return new ApiResponse<string>(HttpStatusCode.BadRequest, "OTP is required");
            }

            var confirmOtp = await _unitofwork.OtpRepository.FirstOrDefaultAsync(o => o.otp == request.otpDto.otp);
            if (confirmOtp == null)
            {
                _logger.LogWarning("Invalid OTP attempt: {OTP}", request.otpDto.otp);
                return new ApiResponse<string>(HttpStatusCode.NotFound, "Invalid OTP");
            }

            if (confirmOtp.IsExpier)
            {
                _logger.LogWarning("Expired OTP attempt: {OTP}", request.otpDto.otp);
                return new ApiResponse<string>(HttpStatusCode.BadRequest, "OTP is expired");
            }

            if (confirmOtp.IsUsed)
            {
                _logger.LogWarning("OTP already used: {OTP}", request.otpDto.otp);
                return new ApiResponse<string>(HttpStatusCode.BadRequest, "OTP already used");
            }

            var user = await _unitofwork.UserRepository.FirstOrDefaultAsync(u => u.Email == confirmOtp.UserEmail);
            if (user == null)
            {
                _logger.LogWarning("User not found for OTP: {OTP}", request.otpDto.otp);
                return new ApiResponse<string>(HttpStatusCode.NotFound, "User not found");
            }

            var accessToken = _tokenSerivce.GenerateJwtToken(user);

            var userRefreshToken = await _unitofwork.RefreshTokenRepository.FirstOrDefaultAsync(
                rt => rt.userId == user.Id,
                o => o.OrderByDescending(m => m.CreatedOn));

            if (userRefreshToken != null && userRefreshToken.IsActive)
            {
                _tokenSerivce.WriteAuthTokenAtHttpOnlyCookie("ACCESS_TOKEN", accessToken.jwtToken, accessToken.expierAtUtc);
                _tokenSerivce.WriteAuthTokenAtHttpOnlyCookie("REFRESH_TOKEN", userRefreshToken.Token, DateTime.UtcNow.AddDays(3));

                _logger.LogInformation("User {UserEmail} logged in successfully with existing refresh token", user.Email);
                return new ApiResponse<string>(HttpStatusCode.OK, "Login successful", $"Welcome {user.userName}");
            }

            // Create new refresh token
            RefreshToken refreshToken = _tokenSerivce.GenerateRefreshToken();
            refreshToken.userId = user.Id;

            await _unitofwork.RefreshTokenRepository.CreateAsync(refreshToken);

            _tokenSerivce.WriteAuthTokenAtHttpOnlyCookie("ACCESS_TOKEN", accessToken.jwtToken, accessToken.expierAtUtc);
            _tokenSerivce.WriteAuthTokenAtHttpOnlyCookie("REFRESH_TOKEN", refreshToken.Token, DateTime.UtcNow.AddDays(3));

            confirmOtp.IsUsed = true;
            await _unitofwork.OtpRepository.UpdateAsync(confirmOtp);

            _logger.LogInformation("User {UserEmail} logged in successfully and OTP marked as used", user.Email);

            return new ApiResponse<string>(HttpStatusCode.OK, "Login successful", $"Welcome {user.userName}");
        }
    }
}
