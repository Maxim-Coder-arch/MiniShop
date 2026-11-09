using MiniShop.Api.Models;

namespace MiniShop.Api.Services;
public class OrderCalculator
{
    private const decimal FirstDiscountThreshold = 1000m;
    private const decimal SecondDiscountThreshold = 5000m;

    private const decimal FirstDiscountRate = 10m;
    private const decimal SecondDiscountRate = 15m;

    public OrderReceipt Calculate(OrderRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidateRequest(request);

        decimal subtotal = CalculateSubtotal(request.Price, request.Quantity);

        decimal discountPercentage = CalculateDiscount(subtotal);

        decimal discountAmount = CalculateDiscountAmount(
            subtotal,
            discountPercentage
        );

        decimal total = CalculateTotal(subtotal, discountAmount);

        return CreateReceipt(
            request,
            subtotal,
            discountPercentage,
            discountAmount,
            total
        );
    }

    private static void ValidateRequest(OrderRequest request)
    {
        if (request.Price < 0)
        {
            throw new ArgumentException("Цена товара не может быть отрицательной.");
        }

        if (request.Quantity < 1)
        {
            throw new ArgumentException("Количество товара должно быть не меньше единицы.");
        }
    }

    private static decimal CalculateSubtotal(decimal price, int quantity)
    {
        return price * quantity;
    }

    private static decimal CalculateDiscount(decimal subtotal)
    {
        if (subtotal >= SecondDiscountThreshold)
        {
            return SecondDiscountRate;
        }

        if (subtotal >= FirstDiscountThreshold)
        {
            return FirstDiscountRate;
        }

        return 0m;
    }

    private static decimal CalculateDiscountAmount(decimal subtotal, decimal discountPercentage)
    {
        return Math.Round(subtotal * discountPercentage / 100m, 2, MidpointRounding.AwayFromZero);
    }

    private static decimal CalculateTotal(decimal subtotal, decimal discountAmount)
    {
        return subtotal - discountAmount;
    }

    private static OrderReceipt CreateReceipt(
        OrderRequest request,
        decimal subtotal,
        decimal discountPercentage,
        decimal discountAmount,
        decimal total)
    {
        return new OrderReceipt
        {
            ProductName = request.ProductName,
            PricePerUnit = request.Price,
            Quantity = request.Quantity,
            Subtotal = subtotal,
            DiscountPercentage = discountPercentage,
            DiscountAmount = discountAmount,
            Total = total
        };
    }
}
