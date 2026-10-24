using InventoryService.Application.Features.Dtos;
using InventoryService.Application.Features.Queries;
using InventoryService.Application.Repositories;
using InventoryService.Domain.Common;
using MediatR;

namespace InventoryService.Application.Features.Handlers;

public sealed class GetInventoryQueryHandler(IInventoryRepository repository)
    : IRequestHandler<GetInventoryQuery, Result<List<InventoryItemDto>>>
{
    public async Task<Result<List<InventoryItemDto>>> Handle(GetInventoryQuery request, CancellationToken ct)
    {
        var items = await repository.GetAllAsync(ct);
        return items.Select(x => new InventoryItemDto(x.ProductId, x.Quantity)).ToList();
    }
}