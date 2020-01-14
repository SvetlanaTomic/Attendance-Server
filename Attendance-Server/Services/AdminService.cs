using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AttendanceServer.Entities;
using AttendanceServer.Helpers;

namespace AttendanceServer.Services
{
    public interface IAdminService
    {
        Admin Authenticate(Admin admin);
    }

    public class AdminService : IAdminService
    {
      

        private readonly AppSettings _appSettings;

        public AdminService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Admin Authenticate(Admin admin)
        {
           

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, admin.AdminId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            admin.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            admin.Password = null;

            return admin;
        }

     
    }
}

