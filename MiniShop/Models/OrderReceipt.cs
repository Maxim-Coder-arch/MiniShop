namespace MiniShop.Api.Models;
public class OrderReceipt
{
    public string ProductName { get; set; } = string.Empty;
    public decimal PricePerUnit { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Total { get; set; }
}
