using Order.API.Models.Requests;

namespace Order.API.Services;

public interface IOrderManagementService
{
    public Task CreateOrderAsync(OrderCreatedRequestModel requestModel);
}