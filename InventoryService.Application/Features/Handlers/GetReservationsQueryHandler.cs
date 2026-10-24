using InventoryService.Application.Features.Dtos;
using InventoryService.Application.Features.Queries;
using InventoryService.Application.Repositories;
using InventoryService.Domain.Common;
using MediatR;

namespace InventoryService.Application.Features.Handlers;

public sealed class GetReservationsQueryHandler(IInventoryRepository repository)
    : IRequestHandler<GetReservationsQuery, Result<List<ReservationDto>>>
{
    public async Task<Result<List<ReservationDto>>> Handle(
        GetReservationsQuery request, CancellationToken ct)
    {
        var reservations = await repository.GetReservationsAsync(ct);

        return reservations
            .Select(x => new ReservationDto(
                x.Id, x.OrderId, x.ProductId, x.Quantity, x.CreatedDate))
            .ToList();
    }
}