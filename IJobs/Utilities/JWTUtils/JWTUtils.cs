﻿using IJobs.Models;
using IJobs.Models.Base;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IJobs.Utilities.JWTUtils
{
    public class JWTUtils<TEntity> : IJWTUtils<TEntity> where TEntity : BaseEntity
    {
        private readonly AppSettings _appSettings;

        public JWTUtils(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public string GenerateJWTToken(TEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var appPrivateKey = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(appPrivateKey),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Guid ValidateJWTToken(string token)
        {
            if (token == null)
                return Guid.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var appPrivateKey = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(appPrivateKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
            try
            {
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validateToken);
                var jwtToken = (JwtSecurityToken)validateToken;
                var userId = new Guid(jwtToken.Claims.FirstOrDefault(x => x.Type == "id").Value);
                return userId;
            }
            catch(Exception)
            {
                return Guid.Empty;
            }
        }
    }
}
