using Mango.Services.AuthAPI.Models.Dto;
using MangoServicesCouponAPI.Models.Dto;

namespace Mango.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegisterationRequestDTO registerationRequestDTO);
        Task<LoginReponseDto> Login(LoginRequestDTO loginRequestDTO);

    }
}
