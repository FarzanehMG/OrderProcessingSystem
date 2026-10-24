using InventoryService.Application.Repositories;
using InventoryService.Domain.Entities;
using InventoryService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Repositories;

internal sealed class InventoryRepository(InventoryDbContext db) : IInventoryRepository
{
    public Task<InventoryItemEntity?> GetByProductIdAsync(long productId, CancellationToken ct = default)
        => db.InventoryItems.FirstOrDefaultAsync(x => x.ProductId == productId, ct);

    public Task<bool> ReservationExistsAsync(long orderId, CancellationToken ct = default)
        => db.Reservations.AnyAsync(x => x.OrderId == orderId, ct);

    public async Task AddReservationAsync(ReservationEntity reservation, CancellationToken ct = default)
        => await db.Reservations.AddAsync(reservation, ct);

    public Task SaveChangesAsync(CancellationToken ct = default)
        => db.SaveChangesAsync(ct);
}