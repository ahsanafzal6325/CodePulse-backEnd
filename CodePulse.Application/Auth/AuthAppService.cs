using CodePulse.Application.Auth.Dto;
using CodePulse.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.Auth
{
    public class AuthAppService : IAuthAppService
    {
        private UserManager<IdentityUser> _userManager { get; }
        private readonly IConfiguration _configuration;

        public AuthAppService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<RegisterResultDto> RegisterAsync(RegisterRequestDto request)
        {
            try
            {
                var resultDto = new RegisterResultDto();

                var user = new IdentityUser
                {
                    UserName = request.Email,
                    Email = request.Email.Trim()
                };
                var identityResult = await _userManager.CreateAsync(user, request.Password);
                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, UserRolesEnum.Reader.GetDescription());
                    resultDto.Success = true;
                }
                else
                {
                    resultDto.Success = false;
                    resultDto.Errors.AddRange(identityResult.Errors.Select(e => e.Description));
                }
                return resultDto;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            try
            {
                var identityUser = await _userManager.FindByEmailAsync(request.Email);
                if (identityUser is not null)
                {
                    var checkPasswordResult = await _userManager.CheckPasswordAsync(identityUser, request.Password);
                    if (checkPasswordResult)
                    {
                        var roles = await _userManager.GetRolesAsync(identityUser);
                        var jwtToken = CreateJwtToken(identityUser, roles.ToList());
                        return new LoginResponseDto
                        {
                            Email = request.Email,
                            Roles = roles.ToList(),
                            Token = jwtToken
                        };
                    }
                }
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        private string CreateJwtToken(IdentityUser user , List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Name, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
