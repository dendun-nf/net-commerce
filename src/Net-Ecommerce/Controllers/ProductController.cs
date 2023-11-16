using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net_Ecommerce.Features.Products.GetById;

namespace Net_Ecommerce.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ISender _sender;

    public ProductController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{request.Id:guid}", Order = 0)]
    public async Task<IActionResult> Get([FromRoute] ProductByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return Ok(result);
    }
}