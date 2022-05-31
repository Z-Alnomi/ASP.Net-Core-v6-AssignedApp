﻿using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
        [Route("api/[controller]")]
        public class ProductsController : ControllerBase
        {
                private DataContext _context;

                public ProductsController(DataContext context)
                {
                        _context = context;
                }

                // api/products
                [HttpGet]
                public IAsyncEnumerable<Product> GetProducts()
                {
                        return _context.Products.AsAsyncEnumerable();
                }

                // api/products/1
                [HttpGet("{id}")]
                public async Task<IActionResult> GetProduct(long id)
                {
                        Product product = await _context.Products.FindAsync(id);

                        if (product == null)
                        {
                                return NotFound();
                        }
                        return Ok(product);

                }

                // api/products
                [HttpPost]
                public async Task SaveProduct([FromBody] Product product)
                {
                        await _context.Products.AddAsync(product);
                        await _context.SaveChangesAsync();
                }

                // api/products
                [HttpPut]
                public void UpdateProduct([FromBody] Product product)
                {
                        _context.Update(product);
                        _context.SaveChanges();
                }

                // api/products/1
                [HttpDelete("{id}")]
                public void DeleteProduct(long id)
                {
                        _context.Products.Remove(new Product { Id = id });
                        _context.SaveChanges();
                }
        }
}
