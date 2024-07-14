using MediatR;

namespace Jungle.Api.Features.Category;

internal abstract class CreateCategory
{
    internal class Command : IRequest
    {
        public required string Name { get; set; }
    }

    internal sealed class
}