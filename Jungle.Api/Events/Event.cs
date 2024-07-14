using System.Text.Json.Serialization;

namespace Jungle.Api.Events
{
    [JsonPolymorphic]
    [JsonDerivedType(typeof(CategoryCreated), nameof(CategoryCreated))]
    public abstract class Event
    {
        public abstract Guid StreamId { get; }
        public DateTime CreatedAtUtc { get; set; }

        [JsonPropertyName("PartitionKey")]
        public string PartitionKey => StreamId.ToString();

        [JsonPropertyName("SortKey")]
        public string SortKey => CreatedAtUtc.ToString("O");
    }
}
