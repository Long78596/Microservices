using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using MangoServicesCouponAPI.Models.Dto;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthService(AppDbContext appDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<LoginReponseDto> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
            if(user==null || isValid == false)
            {
                return new LoginReponseDto()
                {
                    User= null,Token= ""
                };
                UserDto userDto = new()
                {
                 Email=user.Email,
                 ID=user.Id,
                 Name=user.Name,
                 PhoneNumber=user.PhoneNumber,
                };
                LoginReponseDto loginResponseDto = new LoginReponseDto()
                {
                    User = userDto,
                    Token = "",
                };

                return loginResponseDto;
            }
        }

        public async Task<string> Register(RegisterationRequestDTO registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _appDbContext.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };

                    return "";

                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }
    }
}

