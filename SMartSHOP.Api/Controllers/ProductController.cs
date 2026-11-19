using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMartSHOP.Api.Data;
using SMartSHOP.Api.DTOs;
using SMartSHOP.Api.Models;

namespace SMartSHOP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryName = p.Category != null ? p.Category.Name : "Brak kategorii"
            })
            .ToListAsync();

        return Ok(products);
    }
}