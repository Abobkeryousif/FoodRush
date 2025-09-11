
namespace FoodRush.Application.Feature.Command.Account
{
    public record ResetPasswordCommand(ResetPasswordDto passwordDto) : IRequest<ApiResponse<string>>;
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;

        public ResetPasswordCommandHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        

        public async Task<ApiResponse<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitofwork.UserRepository.FirstOrDefaultAsync(u=> u.Email == request.passwordDto.email);
            if (user == null)
                return new ApiResponse<string>(HttpStatusCode.NotFound,"Not Found User");

            var verficiationUser = await _unitofwork.VerficiationRepository.FirstOrDefaultAsync(v=> v.Email == user.Email);

            if (verficiationUser.Token != request.passwordDto.token)
                return new ApiResponse<string>(HttpStatusCode.BadRequest,"Invalid Token");

            if (verficiationUser.IsExpier)
                return new ApiResponse<string>(HttpStatusCode.BadRequest,"Expier Token");

            if (verficiationUser.IsUsed)
                return new ApiResponse<string>(HttpStatusCode.BadRequest,"Token Already Used");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.passwordDto.password);
            user.Password = passwordHash;

            await _unitofwork.UserRepository.UpdateAsync(user);
            verficiationUser.IsUsed = true;

            await _unitofwork.VerficiationRepository.UpdateAsync(verficiationUser);

            return new ApiResponse<string>(HttpStatusCode.OK,"Succuss",$"Password Updated Successfly {user.userName}");

        }
    }
}
