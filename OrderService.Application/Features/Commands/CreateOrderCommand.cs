using MediatR;
using OrderService.Domain.Common;

namespace OrderService.Application.Features.Commands;

public record CreateOrderCommand(long ProductId, int Quantity) : IRequest<Result<long>>;