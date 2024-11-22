﻿using Mango.Web.Models;
using MangoServicesCouponAPI.Models.Dto;

namespace Mango.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponAsync(string couponCode);
        Task<ResponseDto?> GetAllCouponAsync();
        Task<ResponseDto?> GetCouponByIdAsync(int id);
        Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);

        Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto);
        Task<ResponseDto?> DeleteCouponAsync(int id);
    }
}
