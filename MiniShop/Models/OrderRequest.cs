using System.ComponentModel.DataAnnotations;

namespace MiniShop.Api.Models;
public class OrderRequest
{
    [Required]
    [StringLength(100)]
    public string ProductName { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
