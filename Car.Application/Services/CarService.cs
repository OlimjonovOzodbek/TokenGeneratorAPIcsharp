using Car.Domain.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Car.Application.Services
{
    public class CarService : ICarService
    {
        private IConfiguration _configuration;

        public CarService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string TokenGenerator(CarModel model)
        {
            SymmetricSecurityKey securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));
            SigningCredentials signingCredentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            int ExpiredDate = int.Parse(_configuration["JWT:Exp"]!);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture),
                ClaimValueTypes.Integer64),
                new Claim("Name",model.Name),
                new Claim("Model",model.Model),
            };
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                (
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudence"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(ExpiredDate),
                    signingCredentials: signingCredentials);
            string Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return $"Your Token: {Token}";

               
            
        }
    }
}
