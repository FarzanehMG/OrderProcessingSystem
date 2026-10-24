using InventoryService.Domain.Common;
using InventoryService.Domain.Errors;

namespace InventoryService.Domain.Entities;

public sealed class ReservationEntity
{
    #region Properties

    public long Id { get; private set; }
    public long OrderId { get; private set; }
    public long ProductId { get; private set; }
    public int Quantity { get; private set; }

    #endregion

    #region Audit

    public DateTimeOffset CreatedDate { get; private set; }
    public string CreatedBy { get; private set; } = null!;

    #endregion

    #region Constructor

    private ReservationEntity() { } // for EF Core

    #endregion

    #region Factory Methods

    public static Result<ReservationEntity> Create(long orderId, long productId, int quantity, string createdBy)
    {
        if (orderId <= 0)
            return InventoryErrors.InvalidOrderId;

        if (productId <= 0)
            return InventoryErrors.InvalidProductId;

        if (quantity <= 0)
            return InventoryErrors.InvalidQuantity;

        return new ReservationEntity
        {
            ProductId = productId,
            OrderId = orderId,
            Quantity = quantity,
            CreatedDate = DateTimeOffset.UtcNow,
            CreatedBy = createdBy
        };
    }

    #endregion
}