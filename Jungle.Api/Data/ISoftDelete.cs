namespace Jungle.Api.Data
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedOnUtc { get; set; }
    }
}
