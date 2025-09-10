

namespace FoodRush.Infrastructure.Implemention
{
    public class TokenService : ITokenSerivce
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IOptions<JwtOptions> options, IHttpContextAccessor httpContextAccessor)
        {
            _jwtOptions = options.Value;   
            _httpContextAccessor = httpContextAccessor;
        }

        public (string jwtToken, DateTime expierAtUtc) GenerateJwtToken(User user)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var cerdintial = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var expier = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiertionTimeInMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expier,
                signingCredentials: cerdintial
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return (jwtToken, expier);
        }

        public RefreshToken GenerateRefreshToken()
        {
            var random = new byte[32];
            using var Generator = new RNGCryptoServiceProvider();
            Generator.GetBytes(random);
            return new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = Convert.ToBase64String(random),
                CreatedOn = DateTime.Now,
                ExpierOn = DateTime.Now.AddDays(3),
            };
        }

        public void WriteAuthTokenAtHttpOnlyCookie(string cookieName, string token, DateTime expiertion)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, token, new CookieOptions
            {
                HttpOnly = true,
                Expires = expiertion,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
        }
    }
}
