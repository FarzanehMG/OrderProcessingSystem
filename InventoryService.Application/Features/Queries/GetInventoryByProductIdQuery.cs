using InventoryService.Application.Features.Dtos;
using InventoryService.Domain.Common;
using MediatR;

namespace InventoryService.Application.Features.Queries;

public record GetInventoryByProductIdQuery(long ProductId) : IRequest<Result<InventoryItemDto>>;