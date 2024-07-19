using Carter;
using Jungle.Api.Data;
using Jungle.Api.Features.Product;
using Jungle.Shared.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jungle.Api.Features.Product
{
    internal abstract class GetProducts
    {
        internal class Query : IRequest<Pagination<ProductDto>>
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        internal sealed class Handler(AppDbContext context) : IRequestHandler<Query, Pagination<ProductDto>>
        {
            public async Task<Pagination<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var products = context.Products
                    .AsNoTracking()
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
                    });

                return await Pagination<ProductDto>
                    .ToPagedListAsync(products, request.Page, request.PageSize);
            }
        }
    }
}

public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products", async (int pageIndex, int pageSize, ISender sender) =>
        {
            var request = new GetProducts.Query
            {
                Page = pageIndex,
                PageSize = pageSize
            };

            var result = await sender.Send(request);

            return result;
        });
    }
}
