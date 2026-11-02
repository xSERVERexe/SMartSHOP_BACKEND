using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMartSHOP.Api.Data;
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
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .ToListAsync();

        return Ok(products);
    }
}