namespace Jungle.Shared.Requests
{
    public class UserInfoDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public bool IsTenant { get; set; }
    }
}
