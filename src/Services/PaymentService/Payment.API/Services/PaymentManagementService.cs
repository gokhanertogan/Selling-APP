using EventBus.Kafka.Abstractions;
using Payment.API.Contexts;

namespace Payment.API.Services;

public class PaymentManagementService(IConsumerService consumerService, ApplicationDbContext context) : IPaymentManagementService
{
    private readonly IConsumerService _consumerService = consumerService;
    private readonly ApplicationDbContext _context = context;
}