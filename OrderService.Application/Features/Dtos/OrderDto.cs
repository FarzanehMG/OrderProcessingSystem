namespace OrderService.Application.Features.Dtos;

public record OrderDto(
    long Id, 
    long ProductId, 
    int Quantity, 
    string Status, 
    DateTimeOffset CreatedDate);