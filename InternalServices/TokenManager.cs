﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace InternalServices
{
    public class TokenManager
    {
        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        public static string GenerateTokenJwt(string correo, int id)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("correo", correo),
                            new Claim("id", id.ToString())
                        }),

                Expires = now.AddMinutes(Convert.ToInt32(30)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                return principal;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public static bool VerificarXCorreo(string token, string correo)
        {
            var principal = TokenManager.GetPrincipal(token);
            var identity = principal?.Identity as ClaimsIdentity;
            var verificar = identity.FindFirst("correo").Value.ToString();
            if (verificar.Equals(correo))
                return true;
            else
                return false;
        }

        public static bool VerificarXId(string token, int id)
        {
            var principal = TokenManager.GetPrincipal(token);
            var identity = principal?.Identity as ClaimsIdentity;
            var verificar = identity.FindFirst("id").Value.ToString();
            if (id.ToString().Equals(verificar))
                return true;
            else
                return false;
        }
    }
}