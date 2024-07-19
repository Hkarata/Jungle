using Carter;
using Carter.OpenApi;
using Jungle.Api.Data;
using Jungle.Shared.Extensions;
using Jungle.Shared.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jungle.Api.Features.Category
{
    internal abstract class GetCategory
    {
        internal class Query : IRequest<Result<CategoryDto>>
        {
            public Guid Id { get; set; }
        }

        internal sealed class Handler(AppDbContext context) : IRequestHandler<Query, Result<CategoryDto>>
        {
            public async Task<Result<CategoryDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var category = await context.Categories
                    .AsNoTracking()
                    .Where(c => c.Id == request.Id)
                    .Include(c => c.Products!)
                    .ThenInclude(c => c.Tenant)
                    .Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Products = c.Products!.Select(p => new ProductDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Quantity = p.Quantity,
                            Price = p.Price,
                            TenantName = p.Tenant!.Name,
                            TenantPhone = p.Tenant.Phone,
                            TenantAddress = p.Tenant.Address
                        }).ToList()
                    })
                    .SingleOrDefaultAsync(cancellationToken);

                if (category is null)
                {
                    return Result.Failure<CategoryDto>(Error.NoneExistentCategory);
                }

                return category;
            }
        }
    }

    public class GetCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/category/{id:guid}", async (Guid id, ISender sender) =>
            {
                var request = new GetCategory.Query { Id = id };
                var result = await sender.Send(request);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result);
            })
                .WithTags("Category")
                .Produces<Result<CategoryDto>>()
                .IncludeInOpenApi();
        }
    }
}
