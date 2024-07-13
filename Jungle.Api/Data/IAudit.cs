namespace Jungle.Api.Data
{
    public interface IAudit
    {
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }

    }
}
