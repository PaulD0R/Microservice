using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UserService.Application.Interfaces.Messages;

namespace UserService.Infrastructure.Kafka;

public class KafkaConsumer<TMessage> : BackgroundService
{
    private readonly string _topic;
    private readonly IConsumer<string, TMessage> _consumer;
    private readonly IMessageHandler<TMessage> _handler;
    private readonly ILogger<KafkaConsumer<TMessage>> _logger;
    
    public KafkaConsumer(
        IOptions<KafkaConsumerSettings> options, 
        IMessageHandler<TMessage> handler, 
        ILogger<KafkaConsumer<TMessage>> logger)
    {
        _logger = logger;
        
        var config = new ConsumerConfig
        {
            BootstrapServers = options.Value.BootstrapServers,
            GroupId = options.Value.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };

        _handler = handler;
        _topic = options.Value.Topic;
        _consumer = new ConsumerBuilder<string, TMessage>(config)
            .SetValueDeserializer(new KafkaValueDeserializer<TMessage>()).Build();
    }

    private async Task? ConsumeAsync(CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                var result = _consumer.Consume(token);
                await _handler.HandleAsync(result.Message.Value, token);
            }
        }
        catch
        {
            _logger.LogError("Consume error");
        }
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() => ConsumeAsync(stoppingToken), stoppingToken);
    }
}