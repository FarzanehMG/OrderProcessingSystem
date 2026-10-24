namespace InventoryService.Application.Features.Dtos;

public record ReservationDto(long Id, long OrderId, long ProductId, int Quantity, DateTimeOffset CreatedDate);