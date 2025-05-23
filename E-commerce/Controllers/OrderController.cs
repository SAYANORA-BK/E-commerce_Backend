﻿using E_commerce.Dto;
using E_commerce.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> OrderItem(CreateOrderDto CreateOrder)
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.Items["UserId"]);
                var Order = await _service.CreateOrder(userId, CreateOrder);
                return Ok("Products Ordered");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{userid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderDetails(int userid)
        {
            try
            {
                var orders = await _service.GetOrdersforAdmin(userid);
                if (orders == null || orders.Count == 0)
                {
                    return Ok("there is no order for this user");
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
              
                int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
                Console.WriteLine(userId);
                Console.WriteLine(userId);

                var result = await _service.GetOrders(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("sales")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> TotalSale()
        {
            try
            {
                var result = await _service.TotalProductSold();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("totalrevenue")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> TotalRevenue()
        {
            try
            {
                var total = await _service.TotalRevenue();
                return Ok(total);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPatch("{orderid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int orderid, string status)
        {
            var statuses = await _service.UpdateOrderStatus(orderid, status);
            return Ok(statuses);
        }

    }
}
