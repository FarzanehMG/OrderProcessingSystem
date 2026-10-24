using OrderService.Domain.Common;
using OrderService.Domain.Enums;
using OrderService.Domain.Errors;

namespace OrderService.Domain.Entities;

public sealed class OrderEntity
{
    #region Properties

    public long Id { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public long ProductId { get; private set; }
    public int Quantity { get; private set; }

    #endregion

    #region Audit

    public DateTimeOffset CreatedDate { get; private set; }
    public string CreatedBy { get; private set; } = null!;
    public DateTimeOffset? ModifiedDate { get; private set; }
    public string? ModifiedBy { get; private set; }

    #endregion

    #region Constructor

    private OrderEntity() { } // for EF Core

    #endregion

    #region Factory Methods

    public static Result<OrderEntity> Create(long productId, int quantity, string createdBy)
    {
        if (productId <= 0)
            return OrderErrors.InvalidProductId;

        if (quantity <= 0)
            return OrderErrors.InvalidQuantity;

        return new OrderEntity
        {
            ProductId = productId,
            Quantity = quantity,
            OrderStatus = OrderStatus.Pending,
            CreatedDate = DateTimeOffset.UtcNow,
            CreatedBy = createdBy
        };
    }

    #endregion

    #region Behaviors

    public Result Confirm(string modifiedBy)
    {
        if (OrderStatus != OrderStatus.Pending)
            return Result.Failure(OrderErrors.InvalidStatusTransition);

        OrderStatus = OrderStatus.Confirmed;
        Touch(modifiedBy);
        return Result.Success();
    }

    public Result Fail(string modifiedBy)
    {
        if (OrderStatus != OrderStatus.Pending)
            return Result.Failure(OrderErrors.InvalidStatusTransition);

        OrderStatus = OrderStatus.Failed;
        Touch(modifiedBy);
        return Result.Success();
    }

    private void Touch(string modifiedBy)
    {
        ModifiedDate = DateTimeOffset.UtcNow;
        ModifiedBy = modifiedBy;
    }

    #endregion
}

