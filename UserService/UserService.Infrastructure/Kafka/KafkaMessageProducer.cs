using Confluent.Kafka;
using Microsoft.Extensions.Options;
using UserService.Application.Interfaces.Messages;

namespace UserService.Infrastructure.Kafka;

public class KafkaMessageProducer<TMessage> : IMessageProducer<TMessage>
{
    private readonly IProducer<string, TMessage> _producer;
    private readonly string _topic;

    public KafkaMessageProducer(IOptions<KafkaSettings> options)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = options.Value.BootstrapServers
        };
        
        _producer = new ProducerBuilder<string, TMessage>(config)
            .SetValueSerializer(new KafkaJsonSerializer<TMessage>()).Build();

        _topic = options.Value.Topic;
    }
    
    public async Task ProduceAsync(TMessage message, CancellationToken cancellationToken)
    {
        await _producer.ProduceAsync(_topic, new Message<string, TMessage>
        {
            Key = "uniq1",
            Value = message
        },  cancellationToken); 
    }

    public void Dispose()
    {
        _producer.Dispose();
    }
}