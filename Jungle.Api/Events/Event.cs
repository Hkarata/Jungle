using Jungle.Api.Events.CategoryEvents;
using Jungle.Api.Events.ProductEvents;
using System.Text.Json.Serialization;

namespace Jungle.Api.Events
{
    [JsonPolymorphic]
    [JsonDerivedType(typeof(CategoryCreated), nameof(CategoryCreated))]
    [JsonDerivedType(typeof(CategoryUpdated), nameof(CategoryUpdated))]
    [JsonDerivedType(typeof(CategoryDeleted), nameof(CategoryDeleted))]
    [JsonDerivedType(typeof(ProductCreated), nameof(ProductCreated))]
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
