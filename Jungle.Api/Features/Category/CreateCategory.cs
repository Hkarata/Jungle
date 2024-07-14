using MediatR;

namespace Jungle.Api.Features.Category;

internal abstract class CreateCategory
{
    internal class Command : IRequest
    {
        public string Name { get; set; }
    }
}