﻿using E_commerce.Dto;
using E_commerce.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _services;
        public ProductController(IProductService services)
        {
            _services = services;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProduct()
        {
            var products = await _services.GetAllProducts();
            return Ok(products);
        }
        [HttpGet("byId/{id}")]
       [Authorize]
        public async Task<IActionResult> GetProductById(int id)
        {
            var products = await _services.GetProductsById(id);
            if (products == null)
            {
                return NotFound("No product in this id");
            }
            return Ok(products);
        }
        [HttpGet("byCategory/{CategoryName}")]
        [Authorize]
        public async Task<IActionResult> GetProductByCategory(string CategoryName)
        {
            var products = await _services.GetProductsByCategory(CategoryName);
            if (products == null || !products.Any())
            {
                return NotFound("No products  in this category.");
            }
            return Ok(products);
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> addProduct([FromForm] AddProductDto addproduct, IFormFile image)
        {
            if (addproduct == null)
            {
                return NotFound("The product is empty");
            }
            bool product = await _services.AddProduct(addproduct, image);
            if (product)
            {
                return Ok(" Added successfully");
            }
            return BadRequest();
        }
        [HttpDelete("{id}/admin")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            bool isdelete = await _services.DeleteProduct(id);
            if (isdelete)
            {
                return Ok("Deleted successfully");
            }
            return NotFound(" no product in this given id");
        }
        [HttpPut("Edit/{id}/admin")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> EditProduct(int id, [FromForm] AddProductDto addproduct, IFormFile image)
        {
            if (addProduct == null)
            {
                return BadRequest("Invalid product data.");
            }
            bool update = await _services.EditProduct(id, addproduct, image);
            if (!update)
            {
                return BadRequest();
            }
            return Ok(" Updated successfully ");
        }
        [HttpGet("{search}")]
        [Authorize]
        public async Task<IActionResult> SearchProduct(string search)
        {
            try
            {
                var products = await _services.SearchProduct(search);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
