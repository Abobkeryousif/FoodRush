
namespace FoodRush.Application.Feature.Command.Account
{
    public record ForgetPasswordCommand(ForgetPasswordDto passwordDto) : IRequest<ApiResponse<string>>;
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, ApiResponse<string>>
    {

        private readonly IUnitofwork _unitofwork;
        private readonly ISendEmailService _sendEmailService;
        private readonly IConfiguration _configuration;
        public ForgetPasswordCommandHandler(IUnitofwork unitofwork, ISendEmailService sendEmailService, IConfiguration configuration)
        {
            _unitofwork = unitofwork;
            _sendEmailService = sendEmailService;
            _configuration = configuration;
        }

        public async Task<ApiResponse<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitofwork.UserRepository.FirstOrDefaultAsync(u=> u.Email == request.passwordDto.email);
            if (user == null)
                return new ApiResponse<string>(HttpStatusCode.NotFound,"Not Found Any User");

            var verficiation = new Verficiation
            {
                Email = request.passwordDto.email,
                Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
                tokenPerpouse = TokenPerpouse.RestPassword,
                ExpierOn = DateTime.Now.AddMinutes(30)
            };

            await _unitofwork.VerficiationRepository.CreateAsync(verficiation);

            var url = $"{_configuration["AppUrl"]}/Authentication/ResetPassword?email={verficiation.Email}&token={verficiation.Token}";

            _sendEmailService.SendEmail(verficiation.Email, "Reset Password", url);

            return new ApiResponse<string>(HttpStatusCode.OK,"Success", "We Send Url In Your Email To Reset Yuor Password Please Check It");
                 
        }
    }
}
