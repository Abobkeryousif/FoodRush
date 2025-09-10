
using FoodRush.Application.Contract.Service;
using FoodRush.Domain.Entites;

namespace FoodRush.Application.Feature.Command.Account
{
    public record CompleteLoginCommand(UserOtpDto otpDto) : IRequest<ApiResponse<string>>;
    public class CompleteLoginCommandHandler : IRequestHandler<CompleteLoginCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;
        private readonly ITokenSerivce _tokenSerivce;

        public CompleteLoginCommandHandler(IUnitofwork unitofwork, ITokenSerivce tokenSerivce)
        {
            _unitofwork = unitofwork;
            _tokenSerivce = tokenSerivce;
        }

        public async Task<ApiResponse<string>> Handle(CompleteLoginCommand request, CancellationToken cancellationToken)
        {
            if (request.otpDto == null || string.IsNullOrEmpty(request.otpDto.otp))
                return new ApiResponse<string>(HttpStatusCode.BadRequest, "OTP is required");

            var confirmOtp = await _unitofwork.OtpRepository.FirstOrDefaultAsync(o => o.otp == request.otpDto.otp);
            if (confirmOtp == null)
                return new ApiResponse<string>(HttpStatusCode.NotFound, "Invalid OTP");

            if (confirmOtp.IsExpier)
                return new ApiResponse<string>(HttpStatusCode.BadRequest, "Otp is expired");

            if (confirmOtp.IsUsed)
                return new ApiResponse<string>(HttpStatusCode.BadRequest, "Otp already used");

            var user = await _unitofwork.UserRepository.FirstOrDefaultAsync(u => u.Email == confirmOtp.UserEmail);
            if (user == null)
                return new ApiResponse<string>(HttpStatusCode.NotFound, "User not found");

            var accessToken = _tokenSerivce.GenerateJwtToken(user);

            var userRefreshToken = await _unitofwork.RefreshTokenRepository.FirstOrDefaultAsync(
                rt => rt.userId == user.Id,
                o => o.OrderByDescending(m => m.CreatedOn));

            if (userRefreshToken != null && userRefreshToken.IsActive)
            {
                _tokenSerivce.WriteAuthTokenAtHttpOnlyCookie("ACCESS_TOKEN", accessToken.jwtToken, accessToken.expierAtUtc);
                _tokenSerivce.WriteAuthTokenAtHttpOnlyCookie("REFRESH_TOKEN", userRefreshToken.Token, DateTime.UtcNow.AddDays(3));
                return new ApiResponse<string>(HttpStatusCode.OK, "Login successful", $"Welcome {user.userName}");
            }
           
                RefreshToken refreshToken = _tokenSerivce.GenerateRefreshToken();
                refreshToken.userId = user.Id;

                await _unitofwork.RefreshTokenRepository.CreateAsync(refreshToken);

                _tokenSerivce.WriteAuthTokenAtHttpOnlyCookie("ACCESS_TOKEN", accessToken.jwtToken, accessToken.expierAtUtc);
                _tokenSerivce.WriteAuthTokenAtHttpOnlyCookie("REFRESH_TOKEN", refreshToken.Token, DateTime.UtcNow.AddDays(3));

                confirmOtp.IsUsed = true;
                await _unitofwork.OtpRepository.UpdateAsync(confirmOtp);

                return new ApiResponse<string>(HttpStatusCode.OK, "Login successful", $"Welcome {user.userName}");
           
        }


    }
}

