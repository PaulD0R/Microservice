using Confluent.Kafka;
using HotelService.Application.Interfaces.Messages;
using HotelService.Infrastructure.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace HotelService.Infrastructure.Kafka;

public class KafkaConsumer<TMessage> : BackgroundService
{
    private readonly string _topic; 
    private readonly IConsumer<string, TMessage> _consumer;
    private readonly IMessageHandler<TMessage> _handler;

    public KafkaConsumer(IOptions<KafkaOptions> kafkaOptions, IMessageHandler<TMessage> handler)
    {
        _handler = handler;
        _topic = kafkaOptions.Value.Topic;
        
        var config = new ConsumerConfig
        {
            BootstrapServers = kafkaOptions.Value.BootstrapServers,
            GroupId = kafkaOptions.Value.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };
        _consumer = new ConsumerBuilder<string, TMessage>(config)
            .SetValueDeserializer(new KafkaDeserializer<TMessage>()).Build();
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() => ConsumeAsync(stoppingToken), stoppingToken);
    }

    private async Task? ConsumeAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(stoppingToken);
                await _handler.HandleAsync(result.Message.Value, stoppingToken);
            }
        }
        catch
        {
            //
        }
    }
}