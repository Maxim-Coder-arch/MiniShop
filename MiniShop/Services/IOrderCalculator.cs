using MiniShop.Api.Models;

namespace MiniShop.Api.Services;
public interface IOrderCalculator
{
    OrderReceipt Calculate(OrderRequest request);
}
