using Mango.Web.Models;
using MangoServicesCouponAPI.Models.Dto;

namespace Mango.Web.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
