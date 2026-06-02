namespace AgroSense.Application.Dtos.Users
{
    public class UserResponse
    {
        public string Id { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsActive { get; set; }

        public bool MustChangePassword { get; set; }

        public IList<string> Roles { get; set; } = new List<string>();
    }
}
