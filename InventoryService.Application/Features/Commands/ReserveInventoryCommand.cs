using InventoryService.Domain.Common;
using MediatR;

namespace InventoryService.Application.Features.Commands;

public record ReserveInventoryCommand(long OrderId, long ProductId, int Quantity)
    : IRequest<Result>;