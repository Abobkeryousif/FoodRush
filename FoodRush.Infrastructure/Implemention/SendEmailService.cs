
namespace FoodRush.Infrastructure.Implemention
{
    public class SendEmailService : ISendEmailService
    {
        private readonly MailSetting _setting;
        public SendEmailService(IOptions<MailSetting> options)
        {
            _setting = options.Value;
        }
        public void SendEmail(string mailTo, string Subject, string Message)
        {
            using (var client = new SmtpClient())
            {
                client.Connect(_setting.Host, _setting.Port);
                client.Authenticate(_setting.Email, _setting.Password);

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = Message,
                    TextBody = "Hello"
                };

                var message = new MimeMessage
                {
                    Body = bodyBuilder.ToMessageBody()
                };

                message.From.Add(new MailboxAddress("Food Rush",_setting.Email));
                message.To.Add(new MailboxAddress("Welcome",mailTo));
                message.Subject = Subject;
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
