using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Url_RAP_checker.Models.Db;

namespace Url_RAP_checker.Models.User
{
    public class Login
    {
        private readonly URLContext _context;
        public Login(URLContext uRLContext)
        {
            _context = uRLContext;
        }

        public async Task<Token> LoginUser(Users user)
        {
            var result = _context.Users.Where(e => e.Email == user.Email && e.PassWord == user.PassWord)
                .FirstOrDefault();
            Token T = new Token();
            if (result != null)
            {
                T.Email = result.Email;
                T.token = CreateJwt(user);
            }
            else
            {
                T.Email = "-";
                T.token = "-";
            }
            return T;

        }
        private string CreateJwt(Users user)
        {
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qwedrftgxcxckmklkm"));
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.PassWord)
            };

            var _SigningCredential = new SigningCredentials(
                Key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = _SigningCredential
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
