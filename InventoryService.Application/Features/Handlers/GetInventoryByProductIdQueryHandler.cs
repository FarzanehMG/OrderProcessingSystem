using InventoryService.Application.Features.Dtos;
using InventoryService.Application.Features.Queries;
using InventoryService.Application.Repositories;
using InventoryService.Domain.Common;
using InventoryService.Domain.Errors;
using MediatR;

namespace InventoryService.Application.Features.Handlers;

public sealed class GetInventoryByProductIdQueryHandler(IInventoryRepository repository)
    : IRequestHandler<GetInventoryByProductIdQuery, Result<InventoryItemDto>>
{
    public async Task<Result<InventoryItemDto>> Handle(GetInventoryByProductIdQuery request, CancellationToken ct)
    {
        var item = await repository.GetByProductIdAsync(request.ProductId, ct);
        if (item is null)
            return InventoryErrors.ProductNotFound(request.ProductId);

        return new InventoryItemDto(item.ProductId, item.Quantity);
    }
}