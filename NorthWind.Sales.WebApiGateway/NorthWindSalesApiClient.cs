namespace NorthWind.Sales.WebApiGateway;

public class NorthWindSalesApiClient
{
    const string CreateOrderEndPoint = "create";
    readonly HttpClient Client;

    public NorthWindSalesApiClient(HttpClient client)
    {
        Client = client;
    }

    public async Task<int> CreateOrderAsync(CreateOrderDto order)
    {
        int orderId = 0;
        HttpResponseMessage response = await Client.PostAsJsonAsync(CreateOrderEndPoint, order);
        if (response.IsSuccessStatusCode)
        {
            orderId = await response.Content.ReadFromJsonAsync<int>();
        }
        else
        {
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            throw new ProblemDetailsException(jsonResponse);
        }
        return orderId;
    }
}
