namespace AgroSense.Application.Dtos.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public IList<string> Roles { get; set; } = new List<string>();

        public bool MustChangePassword { get; set; }
    }
}
