using Carter;
using Carter.OpenApi;
using Jungle.Api.Authentication;
using Jungle.Api.Data;
using Jungle.Api.Events.CategoryEvents;
using Jungle.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jungle.Api.Features.Category
{
    internal abstract class DeleteCategory
    {
        internal class Command : IRequest<Result>
        {
            public Guid Id { get; set; }
        }

        internal sealed class Handler(AppDbContext context, EventDatabase eventDatabase) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = await context.Categories
                    .FirstOrDefaultAsync(c => c.Id == request.Id);

                if (category is null)
                {
                    return Result.Failure(Error.NoneExistentCategory);
                }

                category.IsDeleted = true;
                category.DeletedOnUtc = DateTime.UtcNow;

                await context.SaveChangesAsync(cancellationToken);

                await eventDatabase.AppendAsync(new CategoryDeleted
                {
                    CategoryId = category.Id,
                    Name = category.Name
                }, "CategoryEvents");

                return Result.Success();
            }
        }
    }

    public class DeleteCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/api/category/{id}", async (Guid id, ISender sender) =>
            {
                var request = new DeleteCategory.Command
                {
                    Id = id
                };

                var result = await sender.Send(request);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result);
            })
                .RequireAuthorization(policy =>
                {
                    policy.RequireClaim(IdentityConstants.AdminClaim, "true")
                        .RequireAuthenticatedUser();
                })
                .Produces<Result>()
                .WithTags("Category")
                .IncludeInOpenApi();
        }
    }
}
