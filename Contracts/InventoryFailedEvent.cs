namespace Contracts;

public record InventoryFailedEvent(long OrderId, string Reason);