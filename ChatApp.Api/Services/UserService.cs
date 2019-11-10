using AutoMapper;
using ChatApp.Api.Configuration;
using ChatApp.Api.Database;
using ChatApp.Api.Models.Domain;
using ChatApp.Api.Models.Dto.User;
using ChatApp.Api.Models.ViewModel.User;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Api.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly ChatContext _context;
        private readonly JwtSettings _jwtSettings;

        public UserService(ChatContext context, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _mapper = mapper;
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AppUser> GetUser(ClaimsPrincipal principal)
        {
            string id = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
            {
                return null;
            }

            return await _context.Users.FindAsync(int.Parse(id));
        }

        public async Task Register(RegisterDto model)
        {
            AppUser user = _mapper.Map<RegisterDto, AppUser>(model);

            // Generate salt as bytes
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            
            // Generate hashed password as bytes
            byte[] hash = HashPassword(model.Password, salt);

            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.CreatedAt = DateTime.Now;

            // Save user to database
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        
        public async Task<AuthenticatedUser> Login(LoginDto model)
        {
            AppUser user = await _context.Users
                .AsNoTracking()
                .Include(c => c.ProfileImage)
                .SingleOrDefaultAsync(u => u.Email.ToLower() == model.Email.ToLower());

            if (user == null)
            {
                return null;
            }

            bool passwordCorrect = VerifyPassword(user.PasswordHash, user.PasswordSalt, model.Password);

            if (!passwordCorrect)
            {
                return null;
            }

            string token = GenerateToken(user);

            AuthenticatedUser result = new AuthenticatedUser
            {
                User = user,
                Token = token,
            };

            return result;
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        private string GenerateToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]   
                {                                          
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.DisplayName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                }),                                        
                Expires = DateTime.UtcNow.AddDays(7),      
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature
                )
            };                                             
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
                                                           
            return tokenHandler.WriteToken(token);         
        }                                                  

        private byte[] HashPassword(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 32);
        }

        private bool VerifyPassword(byte[] hash, byte[] salt, string password)
        {
            return hash.SequenceEqual(HashPassword(password, salt));
        }
    }
}
