﻿using Microsoft.Extensions.Configuration;
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
    public class JwtTokenGenerator:ITokenGenerator
    {
        private IConfiguration _configuration;
        public JwtTokenGenerator(IConfiguration configuration) => _configuration = configuration;

        public string Generate(UserProfileEntity userProfile)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userProfile.UserName)
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"], 
                null, 
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}