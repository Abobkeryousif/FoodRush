
namespace FoodRush.Application.Feature.Command.Users
{
    public record PatchUserEmailCommand(int id,UpdateUserEmailDto userDto) : IRequest<ApiResponse<string>>;
    public class PatchUserEmailCommandHandler : IRequestHandler<PatchUserEmailCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;
        public PatchUserEmailCommandHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        
        public async Task<ApiResponse<string>> Handle(PatchUserEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitofwork.UserRepository.FirstOrDefaultAsync(u=> u.Id == request.id);
            if (user == null)
                return new ApiResponse<string>(HttpStatusCode.NotFound,$"Not Found With ID: {request.id}");

            user.Email = request.userDto.email;
            await _unitofwork.UserRepository.UpdateAsync(user);

            return new ApiResponse<string>(HttpStatusCode.OK,"Success",$"New {user.userName} Email is {user.Email}");
        }
    }
}
