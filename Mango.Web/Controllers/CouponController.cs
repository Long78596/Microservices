﻿using Mango.Web.Models;
using Mango.Web.Services.IServices;
using MangoServicesCouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public  async  Task<IActionResult> CouponIndex()
        {
            List<CouponDto?> list = new();
            ResponseDto? response = await _couponService.GetAllCouponAsync();
            if(response != null  && response.IsSucess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));


            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> CouponCreate(CouponDto model)
		{
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(model);
                if(response !=null && response.IsSucess)
                {
                    TempData["success"] = "Complete Sucessfully ";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
			return View(model);
		}
        [HttpGet]
        public async Task<IActionResult> CouponDelete(int couponId)
        {
			ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);
			if (response != null && response.IsSucess)
			{
			  CouponDto? model 	 = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(model);

            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.CounponId);
            if(response !=null && response.IsSucess)
            {
                TempData["success"] = "Complete Sucessfully ";
                return RedirectToAction(nameof(CouponIndex));

            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(couponDto);
        }
	}
}
