
namespace FoodRush.Application.Contract.Service
{
    public interface ISendEmailService
    {
        void SendEmail(string mailTo, string Subject, string Message);
    }
}
