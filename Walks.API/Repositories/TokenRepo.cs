﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Walks.API.Repositories
{
    public class TokenRepo : ITokenRepo
    {
        private readonly IConfiguration configuration;

        public TokenRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {


            //generate claims.
            var claims = new List<Claim>(); 

            claims.Add(new Claim(ClaimTypes.Email, user.Email??""));


            //reading roles.
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); 
            }

            //get the key.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]??""));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );


            return new JwtSecurityTokenHandler().WriteToken(token); //returns a string.
        }
    }
}
