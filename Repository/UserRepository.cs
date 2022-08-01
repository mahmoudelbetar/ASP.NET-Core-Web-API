using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParkyAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ParkyAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IOptions<AppSettings> _appSettings;

        public UserRepository(ApplicationDbContext db, IOptions<AppSettings> appSettings)
        {
            this.db = db;
            _appSettings = appSettings;
        }
        public User Authenticate(string username, string password)
        {
            var user = db.Users.SingleOrDefault(u => u.UserName == username && u.Password == password);
            if (user == null)
            {
                return null;
            }
            else
            {
                // Generate Token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                return user;
            }
        }

        public bool IsUniqueUser(string username)
        {
            bool isExists = db.Users.Any(u => u.UserName == username);
            if (!isExists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User Register(string username, string password)
        {
            var user = new User()
            {
                UserName = username,
                Password = password,
                Role = "Admin"
            };
            db.Users.Add(user);
            db.SaveChanges();
            return user;
        }
    }
}
