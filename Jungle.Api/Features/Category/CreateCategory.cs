using Carter;
using Carter.OpenApi;
using Jungle.Api.Data;
using Jungle.Api.Events;
using Jungle.Shared.Extensions;
using Jungle.Shared.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jungle.Api.Features.Category;

internal abstract class CreateCategory
{
    internal class Command : IRequest<Result<Guid>>
    {
        public required string Name { get; set; }
    }

    internal sealed class Handler(AppDbContext context, EventDatabase eventDatabase) : IRequestHandler<Command, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var categoryExist = await context.Categories
                 .AnyAsync(c => c.Name.ToLower() == request.Name.ToLower());

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

            await eventDatabase.AppendAsync(new CategoryCreated
            {
                CategoryId = category.Id,
                Name = category.Name
            }, "CategoryEvents");

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

            return Results.Ok(result);
        })
            .WithTags("Category")
            .Produces<Result<Guid>>()
            .IncludeInOpenApi();
    }
}