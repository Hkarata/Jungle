namespace Jungle.Api.Events.CategoryEvents
{
    public class CategoryDeleted : Event
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public override Guid StreamId => CategoryId;
    }
}
