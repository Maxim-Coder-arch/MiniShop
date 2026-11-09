
using MiniShop.Api.Models;
using MiniShop.Api.Services;

namespace MiniShop.Tests;
public class OrderCalculatorTests
{
    private readonly OrderCalculator _calculator;

    public OrderCalculatorTests()
    {
        _calculator = new OrderCalculator();
    }

    [Fact]
    public void Calculate_WithoutDiscount_ReturnsFullPrice()
    {
        var request = new OrderRequest
        {
            ProductName = "Мышь",
            Price = 100m,
            Quantity = 2
        };

        var result = _calculator.Calculate(request);

        Assert.Equal(200m, result.Subtotal);
        Assert.Equal(0m, result.DiscountPercentage);
        Assert.Equal(0m, result.DiscountAmount);
        Assert.Equal(200m, result.Total);
    }

    [Fact]
    public void Calculate_WithTenPercentDiscount_ReturnsCorrectTotal()
    {
        var request = new OrderRequest
        {
            ProductName = "Клавиатура",
            Price = 500m,
            Quantity = 2
        };

        var result = _calculator.Calculate(request);

        Assert.Equal(1000m, result.Subtotal);
        Assert.Equal(10m, result.DiscountPercentage);
        Assert.Equal(100m, result.DiscountAmount);
        Assert.Equal(900m, result.Total);
    }

    [Fact]
    public void Calculate_WithFifteenPercentDiscount_ReturnsCorrectTotal()
    {
        var request = new OrderRequest
        {
            ProductName = "Монитор",
            Price = 2500m,
            Quantity = 2
        };

        var result = _calculator.Calculate(request);

        Assert.Equal(5000m, result.Subtotal);
        Assert.Equal(15m, result.DiscountPercentage);
        Assert.Equal(750m, result.DiscountAmount);
        Assert.Equal(4250m, result.Total);
    }

    [Fact]
    public void Calculate_WithNegativePrice_ThrowsArgumentException()
    {
        var request = new OrderRequest
        {
            ProductName = "Мышь",
            Price = -100m,
            Quantity = 2
        };

        Assert.Throws<ArgumentException>(
            () => _calculator.Calculate(request)
        );
    }

    [Fact]
    public void Calculate_WithZeroQuantity_ThrowsArgumentException()
    {
        var request = new OrderRequest
        {
            ProductName = "Мышь",
            Price = 100m,
            Quantity = 0
        };

        Assert.Throws<ArgumentException>(
            () => _calculator.Calculate(request)
        );
    }

    [Fact]
    public void Calculate_WithNegativeQuantity_ThrowsArgumentException()
    {
        var request = new OrderRequest
        {
            ProductName = "Мышь",
            Price = 100m,
            Quantity = -1
        };

        Assert.Throws<ArgumentException>(
            () => _calculator.Calculate(request)
        );
    }

    [Fact]
    public void Calculate_WithSubtotalBelowDiscountThreshold_ReturnsNoDiscount()
    {
        var request = new OrderRequest
        {
            ProductName = "Наушники",
            Price = 999.99m,
            Quantity = 1
        };

        var result = _calculator.Calculate(request);

        Assert.Equal(0m, result.DiscountPercentage);
        Assert.Equal(999.99m, result.Total);
    }

    [Fact]
    public void Calculate_WithNullRequest_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => _calculator.Calculate(null!)
        );
    }

    [Fact]
    public void Calculate_WithSubtotalJustBelowFifteenPercentThreshold_ReturnsTenPercentDiscount()
    {
        var request = new OrderRequest
        {
            ProductName = "Монитор",
            Price = 4999.99m,
            Quantity = 1
        };

        var result = _calculator.Calculate(request);

        Assert.Equal(10m, result.DiscountPercentage);
        Assert.Equal(500m, result.DiscountAmount);
        Assert.Equal(4499.99m, result.Total);
    }
}