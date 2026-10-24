using InventoryService.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.API.Controllers;

[ApiController]
[Route("inventory")]
public class InventoryController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await sender.Send(new GetInventoryQuery(), ct);
        return Ok(result.Value);
    }

    [HttpGet("{productId:long}")]
    public async Task<IActionResult> GetByProductId(long productId, CancellationToken ct)
    {
        var result = await sender.Send(new GetInventoryByProductIdQuery(productId), ct);
        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(new { result.Error.Code, result.Error.Message });
    }

    [HttpGet("reservations")]
    public async Task<IActionResult> GetReservations(CancellationToken ct)
    {
        var result = await sender.Send(new GetReservationsQuery(), ct);
        return Ok(result.Value);
    }
}