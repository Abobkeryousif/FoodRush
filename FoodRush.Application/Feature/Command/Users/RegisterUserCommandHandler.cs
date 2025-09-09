
namespace FoodRush.Application.Feature.Command.Users
{
    public record RegisterUserCommand(UserDto userDto) : IRequest<ApiResponse<string>>;
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;
        
        public RegisterUserCommandHandler(IUnitofwork unitofwork)
        {
            _unitofwork = unitofwork;
            
        }
        public async Task<ApiResponse<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _unitofwork.UserRepository.IsExist(u=> u.Email == request.userDto.Email);
            if (isExist)
                return new ApiResponse<string>(HttpStatusCode.BadRequest,"This User Already Added!");

            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.userDto.Password);
            var user = new User
            {
                firstName = request.userDto.firstName,
                lastName = request.userDto.lastName,
                Phone = request.userDto.Phone,
                Email = request.userDto.Email,
                City = request.userDto.City,
                Address = request.userDto.Address,
                BirthDate = request.userDto.BirthDate,
                Password = hashPassword,
            };

            await _unitofwork.UserRepository.CreateAsync(user);

            return new ApiResponse<string>(HttpStatusCode.OK,"Success",$"Register Complete!: {user.userName}");
            
        }
    }
}
