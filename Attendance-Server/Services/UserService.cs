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
using System.Threading.Tasks;

namespace AttendanceServer.Services
{
    public interface IUserService
    {
        User Authenticate(string Username, string password);
        IEnumerable<User> GetAll();
        Task<User> FindAsync(int id);
        Task<bool> Modify(int id, User user);
        Task<User> Add(User user);
        Task<bool> Remove(User user);
        bool Exist(int id);
    }

    public class UserService : IUserService
    {
        // Users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _Users = new List<User>
        {
            new User { Id = 1, Username = "test", Password = "test" }
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var User = _Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if User not found
            if (User == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, User.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            User.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            User.Password = null;

            return User;
        }

        public IEnumerable<User> GetAll()
        {
            // return Users without passwords
            return _Users.Select(x => {
                x.Password = null;
                return x;
            });
        }

  

        public Task<bool> Modify(int id, User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> Add(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(User user)
        {
            throw new NotImplementedException();
        }

        public bool Exist(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}


