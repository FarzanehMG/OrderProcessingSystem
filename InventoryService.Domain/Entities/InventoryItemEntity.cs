using InventoryService.Domain.Common;
using InventoryService.Domain.Errors;

namespace InventoryService.Domain.Entities;

public sealed class InventoryItemEntity
{
    #region Properties

    public long ProductId { get; private init; }
    public int Quantity { get; private set; }

    #endregion

    #region Audit

    public DateTimeOffset CreatedDate { get; private set; }
    public string CreatedBy { get; private set; } = null!;
    public DateTimeOffset? ModifiedDate { get; private set; }
    public string? ModifiedBy { get; private set; }

    #endregion

    #region Constructor

    private InventoryItemEntity() { } // for EF Core

    #endregion

    #region Factory Methods

    public static Result<InventoryItemEntity> Create(long productId, int quantity, string createdBy)
    {
        if (productId <= 0)
            return InventoryErrors.InvalidProductId;

        if (quantity < 0)
            return InventoryErrors.InvalidStock;

        return new InventoryItemEntity
        {
            ProductId = productId,
            Quantity = quantity,
            CreatedDate = DateTimeOffset.UtcNow,
            CreatedBy = createdBy
        };
    }

    #endregion

    #region Behaviors

    public Result Reserve(int quantity, string modifiedBy)
    {
        if (quantity <= 0)
            return Result.Failure(InventoryErrors.InvalidQuantity);

        if (Quantity < quantity)
            return Result.Failure(InventoryErrors.InsufficientStock(ProductId, quantity, Quantity));

        Quantity -= quantity;
        ModifiedDate = DateTimeOffset.UtcNow;
        ModifiedBy = modifiedBy;
        return Result.Success();
    }

    #endregion
}