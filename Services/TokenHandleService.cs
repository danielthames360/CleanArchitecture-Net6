using CleanArchitecture.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Services
{
    public class TokenHandleService : ITokenHanldeService
    {
        private readonly JwtConfig _jwtConfig;

        public TokenHandleService(IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public async Task<string> GenerateJwtToken(ITokenParameters parms)
        {
            var jwtTokenHanlder = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", parms.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, parms.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, parms.UserName),
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHanlder.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHanlder.WriteToken(token);

            return jwtToken;
        }
    }
}
