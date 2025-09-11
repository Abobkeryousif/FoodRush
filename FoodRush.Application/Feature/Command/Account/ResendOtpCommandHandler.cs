
namespace FoodRush.Application.Feature.Command.Account
{
    public record ResendOtpCommand(ResendOtpInUserEmail emailDto) : IRequest<ApiResponse<string>>;
    public class ResendOtpCommandHandler : IRequestHandler<ResendOtpCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;
        private readonly ISendEmailService _sendEmailService;
        public ResendOtpCommandHandler(IUnitofwork unitofwork, ISendEmailService sendEmailService)
        {
            _unitofwork = unitofwork;
            _sendEmailService = sendEmailService;
        }

        public async Task<ApiResponse<string>> Handle(ResendOtpCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.emailDto.email))
                return new ApiResponse<string>(HttpStatusCode.BadRequest, "Email is required");

            var user = await _unitofwork.UserRepository.FirstOrDefaultAsync(u=> u.Email == request.emailDto.email);
            if (user == null)
                return new ApiResponse<string>(HttpStatusCode.NotFound,"User Not Found");

            //Handel Old OTP
            var oldOtp = await _unitofwork.OtpRepository.FirstOrDefaultAsync(o=> o.UserEmail == user.Email && !o.IsUsed && o.ExpirationOn > DateTime.Now);

            if (oldOtp != null)
            {
                oldOtp.IsUsed = true;
                await _unitofwork.OtpRepository.UpdateAsync(oldOtp);
            }

            //Generate New OTP
            Random random = new Random();
            int otp = random.Next(0, 9999);

            var userOtp = new OTP
            {
                otp = otp.ToString("0000"),
                UserEmail = user.Email,
                ExpirationOn = DateTime.Now.AddMinutes(5),
                IsUsed = false
            };

            await _unitofwork.OtpRepository.CreateAsync(userOtp);

            //Resend New OTP To User Email
            _sendEmailService.SendEmail(user.Email,"Verify 2FA Code",$"Please confirm this code to complete login: {userOtp.otp}");

            return new ApiResponse<string>(HttpStatusCode.OK,"Success","We sent a new 2FA code to your email, please confirm it");
        }
    }
}
