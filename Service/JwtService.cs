using backend.models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Service
{
    public class JwtService
    {
        public string ScretKey { get; set; }
        public int TokenDuration { get; set; }
        private readonly IConfiguration config;

        public JwtService(IConfiguration _config)
        {
            this.config = _config;
            this.ScretKey = config.GetSection("jwtConfig").GetSection("Key").Value;
            this.TokenDuration = Int32.Parse(config.GetSection("jwtConfig").GetSection("Duration").Value);
        }

        public string GenerateToken(string UserName, string UserPassword)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.ScretKey));

            var signature = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var payload = new[]
            {
                new Claim("UserName",UserName),
                new Claim("UserPassword",UserPassword)
            };
            var jwtToken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: payload,
                expires: DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials:signature
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
