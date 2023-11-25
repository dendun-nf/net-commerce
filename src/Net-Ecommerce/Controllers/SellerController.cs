using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Ecommerce.Features.Sellers.GetById;
using Net_Ecommerce.Features.Sellers.GetOrderById;
using Net_Ecommerce.Features.Sellers.GetOrders;

namespace Net_Ecommerce.Controllers;


[ApiController]
[Route("[controller]")]
public class SellerController : ControllerBase
{
    private readonly ISender _sender;

    public SellerController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{request.Id:guid}")]
    public async Task<IActionResult> Get([FromRoute] SellerByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders([FromQuery] OrdersBySellerRequest request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("{request.SellerId:guid}/orders/{request.OrderId:guid}")]
    public async Task<IActionResult> GetOrder([FromRoute] SellerOrderByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        
        return Ok(result);
    }
}