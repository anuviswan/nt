using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nt.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Infrastructure.WebApi.Authentication
{
    public class JwtTokenGenerator
    {
        public IConfiguration Config { get; init; }
        private static Lazy<JwtTokenGenerator> _instance;

        private JwtTokenGenerator()
        {

        }

        public static void Initialize(IConfiguration config)
        {
            if (_instance is not null) return;

            _instance = new Lazy<JwtTokenGenerator>(() => new JwtTokenGenerator
            {
                Config = config
            });
        }


        public static JwtTokenGenerator Instance => _instance.Value;

        public string Generate(UserProfileEntity userProfile)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userProfile.UserName)
            };
            var token = new JwtSecurityToken(Config["Jwt:Issuer"],
                Config["Jwt:Issuer"], 
                claims, 
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
