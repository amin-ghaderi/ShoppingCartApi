using Microsoft.OpenApi.Models;
using ShoppingCartApi.Application.Interfaces;
using ShoppingCartApi.Application.UseCases;
using ShoppingCartApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo { Title = "Shopping Cart API", Version = "v1" });
});

builder.Services.AddSingleton<ICartRepository, CartRepository>();
builder.Services.AddSingleton<CreateCartUseCase>();
builder.Services.AddSingleton<GetCartUseCase>();
builder.Services.AddSingleton<UpdateCartUseCase>();
builder.Services.AddSingleton<DeleteCartUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/hello", () => Results.Text("API is running", "text/plain"));

app.MapGet("/cart/{userId}", (string userId, GetCartUseCase useCase) =>
{
    try
    {
        var cart = useCase.Execute(userId);
        return cart is null ? Results.NotFound() : Results.Ok(cart);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapPost("/cart", (CreateCartRequest? body, CreateCartUseCase useCase) =>
{
    if (body is null || string.IsNullOrWhiteSpace(body.UserId))
    {
        return Results.BadRequest(new { message = "User id is required." });
    }

    try
    {
        var cart = useCase.Execute(body.UserId);
        return Results.Ok(cart);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapPut("/cart/{userId}", (string userId, UpdateCartRequest? body, UpdateCartUseCase useCase) =>
{
    if (body?.Items is null)
    {
        return Results.BadRequest(new { message = "Items array is required." });
    }

    try
    {
        var items = body.Items.ConvertAll(static i => (i.ProductId, i.Quantity));
        var cart = useCase.Execute(userId, items);
        return Results.Ok(cart);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapDelete("/cart/{userId}", (string userId, DeleteCartUseCase useCase) =>
{
    try
    {
        useCase.Execute(userId);
        return Results.NoContent();
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.Run();

record CreateCartRequest(string UserId);

record UpdateCartRequest(List<ItemDto> Items);

record ItemDto(string ProductId, int Quantity);
