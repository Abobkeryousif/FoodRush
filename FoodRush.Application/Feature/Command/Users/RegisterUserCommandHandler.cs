namespace FoodRush.Application.Feature.Command.Users
{
    public record RegisterUserCommand(UserDto userDto) : IRequest<ApiResponse<string>>;

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;
        private readonly ILogger<RegisterUserCommandHandler> _logger;

        public RegisterUserCommandHandler(IUnitofwork unitofwork, ILogger<RegisterUserCommandHandler> logger)
        {
            _unitofwork = unitofwork;
            _logger = logger;
        }

        public async Task<ApiResponse<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start handling RegisterUserCommand for Email: {Email}", request.userDto.Email);

            var isExist = await _unitofwork.UserRepository.IsExist(u => u.Email == request.userDto.Email);
            if (isExist)
            {
                _logger.LogWarning("User already exists with Email: {Email}", request.userDto.Email);
                return new ApiResponse<string>(HttpStatusCode.BadRequest, "This User Already Added!");
            }

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
                Role = "USER"
            };

            await _unitofwork.UserRepository.CreateAsync(user);

            _logger.LogInformation("User registered successfully with Email: {Email}", user.Email);

            return new ApiResponse<string>(
                HttpStatusCode.OK,
                "Success",
                $"Register Complete!: {user.userName}"
            );
        }
    }
}
