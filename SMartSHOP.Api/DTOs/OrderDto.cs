using System.ComponentModel.DataAnnotations;

namespace SMartSHOP.Api.DTOs;

public class CreateOrderItemDto
{
    [Range(1, int.MaxValue)]
    public int ProductId { get; set; }

    [Range(1, 999)]
    public int Quantity { get; set; }
}

public class CreateOrderDto
{
    [EmailAddress]
    [MaxLength(150)]
    public string? CustomerEmail { get; set; }

    [MinLength(1, ErrorMessage = "Zamówienie musi zawierać co najmniej jeden produkt.")]
    public List<CreateOrderItemDto> Items { get; set; } = new();
}

public class OrderItemResponseDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
}

public class OrderResponseDto
{
    public int Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public int UserId { get; set; }
    public string CustomerEmail { get; set; } = string.Empty;
    public List<OrderItemResponseDto> Items { get; set; } = new();
}