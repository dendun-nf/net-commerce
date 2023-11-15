using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Ecommerce.Features.Users.GetById;

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
}