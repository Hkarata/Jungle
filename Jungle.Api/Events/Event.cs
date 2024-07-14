using System.Text.Json.Serialization;

namespace Jungle.Api.Events
{
    [JsonPolymorphic]
    [JsonDerivedType(typeof(CategoryCreated), nameof(CategoryCreated))]
    public abstract class Event
    {
        public abstract Guid StreamId { get; }
        public DateTime CreatedAtUtc { get; set; }

        [JsonPropertyName("PrimaryKey")]
        public string PrimaryKey => StreamId.ToString();

        [JsonPropertyName("StreamKey")]
        public string StreamKey => CreatedAtUtc.ToString("O");
    }
}
