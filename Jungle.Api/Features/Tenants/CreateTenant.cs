using Carter;
using Jungle.Api.Data;
using Jungle.Api.Entities;
using Jungle.Api.Events.TenantEvents;
using Jungle.Shared.Extensions;
using Jungle.Shared.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jungle.Api.Features.Tenants
{
    internal class CreateTenant
    {
        internal class Command : IRequest<Result<Guid>>
        {
            public string Name { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
        }

        internal class Handler(AppDbContext context, EventDatabase eventDatabase) : IRequestHandler<Command, Result<Guid>>
        {
            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var tenantExists = await context.Tenants
                    .AnyAsync(t => t.Name == request.Name);

                if (tenantExists)
                {
                    return Result.Failure<Guid>(Error.DuplicateTenantName);
                }

                var tenantInfoExists = await context.Tenants
                    .AnyAsync(t => t.Phone == request.Phone || t.Email == request.Email);

                if (tenantInfoExists)
                {
                    return Result.Failure<Guid>(Error.DuplicateTenantInfo);
                }

                var tenant = new Tenant
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Phone = request.Phone,
                    Email = request.Email,
                    Address = request.Address,
                    CreatedOnUtc = DateTime.UtcNow,
                    IsDeleted = false
                };

                await context.Tenants.AddAsync(tenant);

                await context.SaveChangesAsync(cancellationToken);

                await eventDatabase.AppendAsync(new TenantCreated
                {
                    TenantId = tenant.Id,
                    Name = tenant.Name,
                    Phone = tenant.Phone,
                    Email = tenant.Email,
                    Address = tenant.Address
                }, "TenantEvents");

                return tenant.Id;
            }
        }
    }

    public class CreateTenantEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/tenant", async (CreateTenantDto tenant, ISender sender) =>
            {
                var request = new CreateTenant.Command
                {
                    Name = tenant.Name,
                    Phone = tenant.Phone,
                    Email = tenant.Email,
                    Address = tenant.Address
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
}
