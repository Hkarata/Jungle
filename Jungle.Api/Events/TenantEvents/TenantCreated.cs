
namespace Jungle.Api.Events.TenantEvents
{
    public class TenantCreated : Event
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public override Guid StreamId => TenantId;
    }
}
