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
        Admin Authenticate(string adminname, string password);
        IEnumerable<Admin> GetAll();
    }

    public class AdminService : IAdminService
    {
        // admins hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<Admin> _admins = new List<Admin>
        {
            new Admin { Id = 1, Username = "test", Password = "test" }
        };

        private readonly AppSettings _appSettings;

        public AdminService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Admin Authenticate(string username, string password)
        {
            var admin = _admins.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if admin not found
            if (admin == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, admin.Id.ToString())
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

        public IEnumerable<Admin> GetAll()
        {
            // return admins without passwords
            return _admins.Select(x => {
                x.Password = null;
                return x;
            });
        }
    }
}

