using MassTransit;
using OrderService.Application.Abstractions;

namespace OrderService.Infrastructure.Messaging;

internal sealed class MassTransitMessagePublisher(IPublishEndpoint publishEndpoint)
    : IMessagePublisher
{
    public Task PublishAsync<T>(T message, CancellationToken ct = default) where T : class
        => publishEndpoint.Publish(message, ct);
}
