

namespace FoodRush.Application.Feature.Command.Users
{
    public record PatchUserRoleCommand(int userId , UserRoleDto roleDto) : IRequest<ApiResponse<string>>;
    public class PatchUserRoleCommandHandler : IRequestHandler<PatchUserRoleCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;

        public PatchUserRoleCommandHandler(IUnitofwork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<ApiResponse<string>> Handle(PatchUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitofwork.UserRepository.GetByIdAsync(request.userId);
            if (user == null)
                return new ApiResponse<string>(HttpStatusCode.NotFound,$"Not Found With ID: {request.userId}");

            var allowedRoles = new[] {"USER", "ADMIN", "SUPERADMIN" };
            if(!allowedRoles.Contains(request.roleDto.Role.ToUpper()))
            {
                return new ApiResponse<string>(HttpStatusCode.BadRequest,"Invalid Role");
            }

            user.Role = request.roleDto.Role.ToUpper();
            await _unitofwork.UserRepository.UpdateAsync(user);

            return new ApiResponse<string>(HttpStatusCode.OK, "Success",$"New Role of {user.userName} is {request.roleDto.Role}");
        }
    }
}
