using JWTAuthorization.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthorization.Services
{
    public class TokenService
    {
        public static string GenerateToken(User user)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(Settings.Secret);

                //Token informations for creation of the Security Token
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    //Pares de chave/valor para ser validado pelo servidor
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role)
                             
                    }),

                    //Expires in 2 hours
                    Expires = DateTime.UtcNow.AddHours(2),

                   //Encriptation Key and Type
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                };

                //Criação do Token a partir das informções do TokenDescriptor
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {

                throw new Exception("Erro ao gerar o token: "+e.Message);
            }

            
        }
    }
}
