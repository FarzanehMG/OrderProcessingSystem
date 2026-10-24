using OrderService.Domain.Common;

namespace OrderService.Domain.Errors;
public static class OrderErrors
{
    public static readonly Error InvalidProductId =
        new("Order.InvalidProductId", "ProductId is invalid.");

    public static readonly Error InvalidQuantity =
        new("Order.InvalidQuantity", "Quantity must be greater than zero.");

    public static readonly Error NotFound =
        new("Order.NotFound", "Order was not found.");

    public static readonly Error InvalidStatusTransition =
        new("Order.InvalidStatusTransition", "Order status cannot be changed from its current state.");
}
