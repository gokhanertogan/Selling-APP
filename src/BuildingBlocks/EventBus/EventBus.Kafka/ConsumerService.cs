using EventBus.Kafka.Abstractions;
using Confluent.Kafka;
using System.Text.Json;

namespace EventBus.Kafka;

public class ConsumerService : IConsumerService
{
    private readonly ConsumerConfig _consumerConfig;
    public delegate Task ProcessMessageAsync(List<IIntegrationEvent> datas);

    public ConsumerService()
    {
        _consumerConfig = new ConsumerConfig()
        {
            GroupId = "my-consumer-group",
            BootstrapServers = "localhost:29092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }
    public async Task ConsumeAsync<T>(string topicName, Func<List<T>, Task> ProcessMessageAsync) where T : IIntegrationEvent
    {
        try
        {
            using var c = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
            c.Subscribe(topicName);

            CancellationTokenSource cts = new();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    try
                    {
                        var response = new List<T>();
                        var cr = c.Consume(cts.Token);
                        var data = JsonSerializer.Deserialize<T>(cr.Message.Value)!;
                        response.Add(data);
                        await ProcessMessageAsync(response);
                        Console.WriteLine($"Message : {cr.Message.Value}");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Message : {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException operationCanceledException)
            {
                Console.WriteLine($"{operationCanceledException.Message}");
            }

            c.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}