using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ShopeeFake.Domain.Entities;
using ShopeeFake.UI.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Infrastructure.Services
{
    public class IdentityServices : IIdentityServices
    {
        private readonly Audience _audience;
        public IdentityServices(
            IOptions<Audience> options)
        {
            _audience = options.Value ?? throw new ArgumentException(nameof(options.Value));
        }
        public string GenerateToken(User user, List<string> roles, int expires)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(roles), //convert roles from list string to json string
                JsonClaimValueTypes.JsonArray)
            };
            var signningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_audience.Secret));
            var jwt = new JwtSecurityToken(
                issuer: _audience.Issuer,
                audience: _audience.Name,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(TimeSpan.FromSeconds(expires)),
                signingCredentials: new SigningCredentials(signningKey,
                SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string GetMD5(string text)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                md5.ComputeHash(Encoding.ASCII.GetBytes(text));
                byte[] result = md5.Hash;
                StringBuilder str = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    str.Append(result[i].ToString("x2"));
                }
                return str.ToString();
            }
        }

        public bool VerifyMD5Hash(string inputHash, string hashVerify)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(inputHash, hashVerify))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
