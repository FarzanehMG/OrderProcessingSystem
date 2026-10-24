using MediatR;
using OrderService.Application.Features.Dtos;
using OrderService.Domain.Common;

namespace OrderService.Application.Features.Queries;

public record GetOrderByIdQuery(long Id) : IRequest<Result<OrderDto>>;