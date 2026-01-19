using Confluent.Kafka;
using HotelService.Application.Interfaces.Messages;
using HotelService.Infrastructure.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HotelService.Infrastructure.Kafka;

public class KafkaConsumer<TMessage> : BackgroundService
{
    private readonly IConsumer<string, TMessage> _consumer;
    private readonly IMessageHandler<TMessage> _handler;
    private ILogger<KafkaConsumer<TMessage>> _logger;

    public KafkaConsumer(
        IOptionsMonitor<KafkaOptions> optionsMonitor, 
        IMessageHandler<TMessage> handler,  
        ILogger<KafkaConsumer<TMessage>> logger)
    {
        _logger = logger;
        _handler = handler;
        
        var options = optionsMonitor.Get(typeof(TMessage).Name);
        var config = new ConsumerConfig
        {
            BootstrapServers = options.BootstrapServers,
            GroupId = options.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };
        _consumer = new ConsumerBuilder<string, TMessage>(config)
            .SetValueDeserializer(new KafkaDeserializer<TMessage>()).Build();
        _consumer.Subscribe(options.Topic);
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() => ConsumeAsync(stoppingToken), stoppingToken);
    }

    private async Task? ConsumeAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {

                var result = _consumer.Consume(stoppingToken);
                await _handler.HandleAsync(result.Message.Value, stoppingToken);
            }
            catch(Exception ex)
            {
                _logger.LogError("Consume error: {ExMessage}", ex.Message);
            }
        }
        _logger.LogError("Consume error: {StoppingTokenIsCancellationRequested}", stoppingToken.IsCancellationRequested);
    }
    
    public override Task StopAsync(CancellationToken cancellationToken) {
        _consumer.Close();
        return base.StopAsync(cancellationToken);
    }
}