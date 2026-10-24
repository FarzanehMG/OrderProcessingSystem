using InventoryService.Domain.Entities;

namespace InventoryService.Application.Repositories;

public interface IInventoryRepository
{
    Task<InventoryItemEntity?> GetByProductIdAsync(long productId, CancellationToken ct = default);
    Task<bool> ReservationExistsAsync(long orderId, CancellationToken ct = default);
    Task AddReservationAsync(ReservationEntity reservation, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);

    Task<List<InventoryItemEntity>> GetAllAsync(CancellationToken ct = default);
    Task<List<ReservationEntity>> GetReservationsAsync(CancellationToken ct = default);
}