﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Models;
using System.Reflection.Metadata.Ecma335;
namespace ProductApp.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = new List<Product>()
            {
                new Product() { Id=1,ProductName="Computer"},
                new Product() { Id=2,ProductName="Keyboard"},
                new Product() { Id=3,ProductName="Mouse"}
            };       
            _logger.LogInformation("GetAllProducts Action has been called.");
            return Ok(products);

        }

        [HttpPost]
        public IActionResult GetAllProducts([FromBody] Product product)
        {

            _logger.LogWarning("Product has been created");
            return StatusCode(201); //Created

        }

    }
}
