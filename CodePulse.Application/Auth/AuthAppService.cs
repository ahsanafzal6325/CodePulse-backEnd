using CodePulse.Application.Auth.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.Auth
{
    public class AuthAppService : IAuthAppService
    {
        private UserManager<IdentityUser> _userManager { get; }

        public AuthAppService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
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
                    await _userManager.AddToRoleAsync(user, "Reader");
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
                        return new LoginResponseDto
                        {
                            Email = request.Email,
                            Roles = roles.ToList(),
                            Token = "TOKEN"
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

    }
}
