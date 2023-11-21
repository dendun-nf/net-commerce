using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Ecommerce.Features.Users.GetById;
using Net_Ecommerce.Features.Users.GetOrderById;
using Net_Ecommerce.Features.Users.GetOrders;

namespace Net_Ecommerce.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    public UserController(ISender sender)
    {
        _sender = sender;
    }
    [HttpGet("{request.Id:guid}", Name = "GetUser", Order = 0)]
    public async Task<IActionResult> Get(
        [FromRoute] UserByIdRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{request.UserId:guid}/orders")]
    public async Task<IActionResult> GetOrders(
        [FromRoute] OrdersByUserRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{request.UserId:guid}/orders/{request.OrderId:guid}")]
    public async Task<IActionResult> GetOrder(
        [FromRoute] UserOrderByIdRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return Ok(result);
    }
}