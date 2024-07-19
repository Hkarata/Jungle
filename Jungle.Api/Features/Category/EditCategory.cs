using Carter;
using Carter.OpenApi;
using Jungle.Api.Data;
using Jungle.Api.Events;
using Jungle.Shared.Extensions;
using Jungle.Shared.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jungle.Api.Features.Category
{
    internal abstract class EditCategory
    {
        internal class Command : IRequest<Result<Guid>>
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        internal sealed class Handler(AppDbContext context, EventDatabase eventDatabase) : IRequestHandler<Command, Result<Guid>>
        {
            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = await context.Categories
                    .FirstOrDefaultAsync(c => c.Id == request.Id);

                if (category is null)
                {
                    return Result.Failure<Guid>(Error.NoneExistentCategory);
                }

                category.Name = request.Name;

                await context.SaveChangesAsync(cancellationToken);

                await eventDatabase.AppendAsync(new CategoryUpdated
                {
                    CategoryId = category.Id,
                    Name = category.Name
                }, "CategoryEvents");

                return category.Id;
            }
        }
    }

    public class EditCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/api/category/{id:guid}", async (Guid id, EditCategoryDto category, ISender sender) =>
            {
                var request = new EditCategory.Command { Id = id, Name = category.Name };

                var result = await sender.Send(request);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result);
            })
                .Produces<Result<Guid>>()
                .WithTags("Category")
                .IncludeInOpenApi();
        }
    }
}
