using InnovaWideTest.Domain.DTOs.Authe;
using InnovaWideTest.Domain.Entities;
using InnovaWideTest.Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InnovaWideTest.Application.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtOptions;

        public AuthService(JwtSettings jwtOptions, UserManager<ApplicationUser> userManager)
        {
            _jwtOptions = jwtOptions;
            _userManager = userManager;
        }

        public async Task<AutheDto> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new AutheDto { Massage = "Not Found!" };

            var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkPassword)
                return new AutheDto { Massage = "Invalid Data!" };

            return new AutheDto
            {
                IsAuthenticated = true,
                Token = GetJwtToken(user),
                UserId = user.Id,
                Email = user.Email,
                Tenent = user.TenantId,
                Name = user.Name,
            };
        }

        public async Task<AutheDto> Register(RegisterDto model)
        {
            var isExist = await _userManager.Users.AnyAsync(a => a.NormalizedEmail == model.Email.ToUpper() || a.TenantId.ToLower() == model.Tenent.ToLower());
            if (isExist)
                return new AutheDto { Massage = "User is already registered!" };

            var user = new ApplicationUser
            {
                Email = model.Email,
                Name = model.Name,
                TenantId = model.Tenent,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return new AutheDto { Massage = string.Join(", ", result.Errors.Select(a => a.Description)) };

            return new AutheDto
            {
                IsAuthenticated = true,
                Massage = "User created successfully!"
            };
        }

        private string GetJwtToken(ApplicationUser user)
        {
            var claims = new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id),
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = DateTime.Now.AddHours(_jwtOptions.DurationInHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)),
                    SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);
            return accessToken;
        }
    }
}
