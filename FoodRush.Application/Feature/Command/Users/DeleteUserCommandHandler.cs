

namespace FoodRush.Application.Feature.Command.Users
{
    public record DeleteUserCommand(int id) : IRequest<ApiResponse<string>>;
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;

        public DeleteUserCommandHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        
        public async Task<ApiResponse<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var deleteUser = await _unitofwork.UserRepository.FirstOrDefaultAsync(u=> u.Id == request.id);
            if (deleteUser == null)
                return new ApiResponse<string>(HttpStatusCode.NotFound,$"Not Found With ID: {request.id}");

            await _unitofwork.UserRepository.DeleteAsync(deleteUser);
            return new ApiResponse<string>(HttpStatusCode.OK,"Success",$"Deleted User: {deleteUser.userName}");
        }
    }
}
