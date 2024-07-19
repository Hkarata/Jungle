using Carter;
using Jungle.Api.Data;
using Jungle.Api.Features.Product;
using Jungle.Shared.Extensions;
using Jungle.Shared.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jungle.Api.Features.Product
{
    internal abstract class GetProduct
    {
        internal class Query : IRequest<Result<ProductDto>>
        {
            public Guid Id { get; set; }
        }

        internal sealed class Handler(AppDbContext context) : IRequestHandler<Query, Result<ProductDto>>
        {
            public async Task<Result<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await context.Products
                    .AsNoTracking()
                    .Where(p => p.Id == request.Id)
                    .Include(p => p.Tenant)
                    .Include(p => p.Categories)
                    .Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Categories = p.Categories!.Select(c => c.Name).ToList(),
                        Images = p.Images,
                        Quantity = p.Quantity,
                        Price = p.Price,
                        TenantName = p.Tenant!.Name,
                        TenantPhone = p.Tenant.Phone,
                        TenantAddress = p.Tenant.Address
                    })
                    .SingleOrDefaultAsync(cancellationToken);

                if (product is null)
                {
                    return Result.Failure<ProductDto>(Error.NoneExistentProduct);
                }

                return product;
            }
        }
    }
}

public class GetProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products/{id}", async (Guid id, ISender sender) =>
        {
            var request = new GetProduct.Query
            {
                Id = id
            };

            var result = await sender.Send(request);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result);
        });
    }
}