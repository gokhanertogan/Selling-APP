namespace Order.API.Models.Requests;
public record OrderCreatedRequestModel(int CustomerId,string Address,decimal Total);