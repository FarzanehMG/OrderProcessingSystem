using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Features.Commands;
using OrderService.Application.Features.Queries;

namespace OrderService.API.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value }, new { id = result.Value })
            : BadRequest(new { result.Error.Code, result.Error.Message });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken ct)
    {
        var result = await sender.Send(new GetOrderByIdQuery(id), ct);
        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(new { result.Error.Code, result.Error.Message });
    }
}
