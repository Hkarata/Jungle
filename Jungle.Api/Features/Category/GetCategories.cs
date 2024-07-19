using Carter;
using Carter.OpenApi;
using Jungle.Api.Data;
using Jungle.Shared.Extensions;
using Jungle.Shared.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jungle.Api.Features.Category
{
    internal abstract class GetCategories
    {
        internal class Query : IRequest<Result<IEnumerable<CategoryDto>>>;

        internal sealed class Handler(AppDbContext context) : IRequestHandler<Query, Result<IEnumerable<CategoryDto>>>
        {
            public async Task<Result<IEnumerable<CategoryDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var categories = await context.Categories
                    .AsNoTracking()
                    .Where(c => !c.IsDeleted)
                    .Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToListAsync(cancellationToken);

                if (categories.Count is 0)
                {
                    return Result.Failure<IEnumerable<CategoryDto>>(Error.NullValue);
                }


                return categories;
            }
        }
    }

    public class GetCategoriesEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/categories", async (ISender sender) =>
            {
                var request = new GetCategories.Query();

                var result = await sender.Send(request);

                if (result.IsFailure)
                {
                    return Results.NoContent();
                }

                return Results.Ok(result);
            })
                .Produces<Result<IEnumerable<CategoryDto>>>()
                .WithTags("Category")
                .IncludeInOpenApi();
        }
    }
}
