using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMartSHOP.Api.Data;
using SMartSHOP.Api.DTOs;
using SMartSHOP.Api.Models;

namespace SMartSHOP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrders()
    {
        var orders = await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new OrderResponseDto
            {
                Id = o.Id,
                Status = o.Status,
                CreatedAt = o.CreatedAt,
                TotalAmount = o.TotalAmount,
                UserId = o.UserId,
                Items = o.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product != null ? oi.Product.Name : $"Produkt #{oi.ProductId}",
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity,
                    LineTotal = oi.UnitPrice * oi.Quantity
                }).ToList()
            })
            .ToListAsync();

        return Ok(orders);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponseDto>> GetOrder(int id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Where(o => o.Id == id)
            .Select(o => new OrderResponseDto
            {
                Id = o.Id,
                Status = o.Status,
                CreatedAt = o.CreatedAt,
                TotalAmount = o.TotalAmount,
                UserId = o.UserId,
                Items = o.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product != null ? oi.Product.Name : $"Produkt #{oi.ProductId}",
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity,
                    LineTotal = oi.UnitPrice * oi.Quantity
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (order == null)
        {
            return NotFound(new { message = $"Zamówienie o ID {id} nie istnieje." });
        }

        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponseDto>> CreateOrder([FromBody] CreateOrderDto dto)
    {
        if (dto.Items == null || dto.Items.Count == 0)
        {
            return BadRequest(new { message = "Zamówienie musi zawierać co najmniej jeden produkt." });
        }

        User? user = null;
        if (!string.IsNullOrWhiteSpace(dto.CustomerEmail))
        {
            var normalizedEmail = dto.CustomerEmail.Trim().ToLower();
            user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);
        }

        user ??= await _context.Users.OrderBy(u => u.Id).FirstOrDefaultAsync();

        if (user == null)
        {
            return BadRequest(new { message = "Brak użytkownika w bazie do przypisania zamówienia." });
        }

        var groupedQuantities = dto.Items
            .GroupBy(i => i.ProductId)
            .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));

        var productIds = groupedQuantities.Keys.ToList();
        var productsFromDb = await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id);

        var missingIds = productIds.Where(id => !productsFromDb.ContainsKey(id)).ToList();
        if (missingIds.Count > 0)
        {
            return BadRequest(new
            {
                message = $"Nie znaleziono produktów o ID: {string.Join(", ", missingIds)}."
            });
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var order = new Order
            {
                UserId = user.Id,
                Status = "New",
                CreatedAt = DateTime.UtcNow
            };

            decimal totalAmount = 0m;

            foreach (var (productId, quantity) in groupedQuantities)
            {
                var product = productsFromDb[productId];
                totalAmount += product.Price * quantity;

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    UnitPrice = product.Price,
                    Quantity = quantity
                });
            }

            order.TotalAmount = totalAmount;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            var response = new OrderResponseDto
            {
                Id = order.Id,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                TotalAmount = order.TotalAmount,
                UserId = order.UserId,
                Items = order.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    ProductId = oi.ProductId,
                    ProductName = productsFromDb[oi.ProductId].Name,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity,
                    LineTotal = oi.UnitPrice * oi.Quantity
                }).ToList()
            };

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, response);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}