namespace AgroSense.Application.Dtos.Users
{
    public class CreateUserRequest
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string TemporaryPassword { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
