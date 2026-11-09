using Microsoft.AspNetCore.Mvc;
using MiniShop.Api.Models;
using MiniShop.Api.Services;

namespace MiniShop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderCalculator _orderCalculator;

    public OrdersController(IOrderCalculator orderCalculator)
    {
        _orderCalculator = orderCalculator;
    }

    [HttpPost("calculate")]
    [ProducesResponseType(typeof(OrderReceipt), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<OrderReceipt> Calculate([FromBody] OrderRequest request)
    {
        OrderReceipt receipt = _orderCalculator.Calculate(request);

        return Ok(receipt);
    }
}