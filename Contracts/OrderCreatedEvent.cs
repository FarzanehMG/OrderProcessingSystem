namespace Contracts;

public record OrderCreatedEvent(long OrderId, long ProductId, int Quantity);