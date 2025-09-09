
namespace FoodRush.Application.Feature.Command.Users
{
    public record UpdateUserCommand(int id,UserDto userDto) : IRequest<ApiResponse<string>>;
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;

        public UpdateUserCommandHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        
        public async Task<ApiResponse<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitofwork.UserRepository.FirstOrDefaultAsync(u=> u.Id == request.id);
            if (user == null)
                return new ApiResponse<string>(HttpStatusCode.NotFound,$"Not Found With ID: {request.id}");

            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.userDto.Password);

            user.firstName = request.userDto.firstName;
            user.lastName = request.userDto.lastName;
            user.Phone = request.userDto.Phone;
            user.Email = request.userDto.Email;
            user.City = request.userDto.City;
            user.Address = request.userDto.Address;
            user.BirthDate = request.userDto.BirthDate;
            user.Password = hashPassword;

            await _unitofwork.UserRepository.UpdateAsync(user);

            return new ApiResponse<string>(HttpStatusCode.OK,"Success","Updated Data Successfly!");
        }
    }
}
