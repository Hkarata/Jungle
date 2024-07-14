using Carter;
using Jungle.Api.Data;
using Jungle.Shared.Extensions;
using Jungle.Shared.Requests;
using MediatR;

namespace Jungle.Api.Features.Category;

internal abstract class CreateCategory
{
    internal class Command : IRequest<Result<Guid>>
    {
        public required string Name { get; set; }
    }

    internal sealed class Handler(AppDbContext context) : IRequestHandler<Command, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var categoryExist = context.Categories
                .Any(x => x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));

            if (categoryExist)
            {
                return Result.Failure<Guid>(Error.ExistentCategory);
            }

            var category = new Entities.Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                IsDeleted = false,
                CreatedOnUtc = DateTime.UtcNow
            };

            await context.Categories.AddAsync(category);

            await context.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}

public class CreateCategoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/category", async (CreateCategoryDto category, ISender sender) =>
        {
            var request = new CreateCategory.Command { Name = category.Name };
            var result = await sender.Send(request);

            if (result.IsFailure)
            {
                return Results.Conflict(result.Error);
            }

            return Results.CreatedAtRoute($"/api/category/{result.Value}");
        });
    }
}