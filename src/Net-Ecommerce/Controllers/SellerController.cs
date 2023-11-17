using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Ecommerce.Features.Sellers.GetById;

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
}