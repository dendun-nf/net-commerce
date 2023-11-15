using Microsoft.AspNetCore.Mvc;
using Net_Ecommerce.Features.Users.GetById;

namespace Net_Ecommerce.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpGet("{request.Id:guid}", Name = "GetUser", Order = 0)]
    public async Task<IActionResult> Get(
        [FromServices] UserByIdHandler handler, 
        [FromRoute] UserByIdRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await handler.Process(request, cancellationToken);
        return Ok(result);
    }
}