
namespace FoodRush.Application.Contract.Service
{
    public interface ITokenSerivce
    {
        (string jwtToken, DateTime expierAtUtc) GenerateJwtToken(User user);
        RefreshToken GenerateRefreshToken();
        void WriteAuthTokenAtHttpOnlyCookie(string cookieName, string token, DateTime expiertion);
    }
}
