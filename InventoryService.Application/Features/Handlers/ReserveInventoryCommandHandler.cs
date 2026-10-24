using InventoryService.Application.Features.Commands;
using InventoryService.Application.Repositories;
using InventoryService.Domain.Common;
using InventoryService.Domain.Entities;
using InventoryService.Domain.Errors;
using MediatR;

namespace InventoryService.Application.Features.Handlers;

public sealed class ReserveInventoryCommandHandler(IInventoryRepository repository)
    : IRequestHandler<ReserveInventoryCommand, Result>
{
    public async Task<Result> Handle(ReserveInventoryCommand request, CancellationToken ct)
    {
        // Idempotency
        if (await repository.ReservationExistsAsync(request.OrderId, ct))
            return Result.Success();

        var item = await repository.GetByProductIdAsync(request.ProductId, ct);
        if (item is null)
            return Result.Failure(InventoryErrors.ProductNotFound(request.ProductId));

        var reserveResult = item.Reserve(request.Quantity, "system");
        if (reserveResult.IsFailure)
            return reserveResult;

        var reservationResult = ReservationEntity.Create(
            request.OrderId, request.ProductId, request.Quantity, "system");
        if (reservationResult.IsFailure)
            return Result.Failure(reservationResult.Error);

        await repository.AddReservationAsync(reservationResult.Value, ct);
        await repository.SaveChangesAsync(ct);

        return Result.Success();
    }
}