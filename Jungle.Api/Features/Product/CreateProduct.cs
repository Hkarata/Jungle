using System.Text.Json;
using Carter;
using Jungle.Api.Data;
using Jungle.Api.Events.ProductEvents;
using Jungle.Shared.Extensions;
using Jungle.Shared.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jungle.Api.Features.Product;

internal abstract class CreateProduct
{
    internal class Command : IRequest<Result<Guid>>
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public List<string>? Images { get; init; }
        public int Quantity { get; init; }
        public decimal Price { get; init; }
        public Guid TenantId { get; init; }
        public List<Guid>? Categories { get; init; }
    }
    
    internal sealed class Handler(AppDbContext context, EventDatabase eventDatabase) : IRequestHandler<Command, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var categories = await  context.Categories
                .Where(x => request.Categories!.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if (categories.Count == 0)
            {
                return Result.Failure<Guid>(Error.ConditionNotMet);
            }

            var product = new Entities.Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Images = JsonSerializer.Serialize(request.Images),
                Quantity = request.Quantity,
                Price = request.Price,
                TenantId = request.TenantId,
                Categories = categories,
                IsDeleted = false,
                CreatedOnUtc = DateTime.UtcNow
            };

            await eventDatabase.AppendAsync(new ProductCreated
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductPrice = product.Price,
                ProductQuantity = product.Quantity,
                Images = product.Images,
                ProductCategories = product.Categories.Select(c => c.Name).ToList()
            }, "ProductEvents");
            
            await context.Products.AddAsync(product, cancellationToken);
            
            await context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}

public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/product", async (CreateProductDto product, ISender sender) =>
        {
            var request = new CreateProduct.Command
            {
                Name = product.Name,
                Description = product.Description,
                Images = product.Images,
                Quantity = product.Quantity,
                Price = product.Price,
                TenantId = product.TenantId,
                Categories = product.Categories
            };

            var result = await sender.Send(request);

            return result.IsFailure ? Results.BadRequest(result.Error) : Results.Ok(result);
        })
        .WithTags("Product")
        .Produces<Result<Guid>>()
        .Produces<Error>(StatusCodes.Status400BadRequest);
    }
}