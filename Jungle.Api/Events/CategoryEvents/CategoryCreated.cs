namespace Jungle.Api.Events.CategoryEvents
{
    public class CategoryCreated : Event
    {
        public required Guid CategoryId { get; set; }
        public required string Name { get; set; }
        public override Guid StreamId => CategoryId;
    }
}
