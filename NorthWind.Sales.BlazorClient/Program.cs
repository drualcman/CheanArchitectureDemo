var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<NorthWindSalesApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["WebApiUri"]);
});
builder.Services.AddScoped<IValidator<CreateOrderDto>, CreateOrderDtoValidator>();
builder.Services.AddScoped<IValidator<CreateOrderDetailDto>, CreateOrderDetailDtoValidator>();

await builder.Build().RunAsync();
