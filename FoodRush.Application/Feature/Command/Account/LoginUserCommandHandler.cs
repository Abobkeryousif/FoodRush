
namespace FoodRush.Application.Feature.Command.Account
{
    public record LoginUserCommand(LoginUserDto userDto) : IRequest<ApiResponse<string>>;
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;

        public LoginUserCommandHandler(IUnitofwork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<ApiResponse<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var loginUser = await _unitofwork.UserRepository.FirstOrDefaultAsync(u=> u.Email == request.userDto.email);
            if (loginUser == null)
                return new ApiResponse<string>(HttpStatusCode.NotFound,"Invalid Email Or Password!");

            var verifyPassword = BCrypt.Net.BCrypt.Verify(request.userDto.password , loginUser.Password);
            if (!verifyPassword)
                return new ApiResponse<string>(HttpStatusCode.NotFound,"Invalid Email Or Password!");

            return new ApiResponse<string>(HttpStatusCode.OK,"Success","We Send 2FA Code In Your Email Please Confirm It");
        }
    }
}

