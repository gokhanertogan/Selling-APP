using EventBus.Kafka.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Kafka;

public static class KafkaServiceRegistration
{
    public static IServiceCollection KafkaServicesRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        KafkaConfig kafkaConfig = configuration.GetSection("KafkaConfig").Get<KafkaConfig>()!;
        services.Configure<KafkaConfig>(options =>
        {
            options = kafkaConfig;
        });

        services.AddSingleton<IProducerService, ProducerService>();
        services.AddSingleton<IConsumerService, ConsumerService>();
        return services;
    }
}