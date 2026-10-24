using InventoryService.Domain.Common;

namespace InventoryService.Domain.Errors;

public static class InventoryErrors
{
    public static readonly Error InvalidProductId =
        new("Inventory.InvalidProductId", "ProductId is invalid.");

    public static readonly Error InvalidQuantity =
        new("Inventory.InvalidQuantity", "Quantity must be greater than zero.");

    public static readonly Error InvalidStock =
        new("Inventory.InvalidStock", "Stock quantity cannot be negative.");

    public static readonly Error InvalidOrderId =
        new("Inventory.InvalidOrderId", "OrderId is invalid.");

    public static Error ProductNotFound(long productId) =>
        new("Inventory.ProductNotFound", $"Product with id {productId} was not found.");

    public static Error InsufficientStock(long productId, int requested, int available) =>
        new("Inventory.InsufficientStock",
            $"Insufficient stock for product {productId}. Requested: {requested}, available: {available}.");
}