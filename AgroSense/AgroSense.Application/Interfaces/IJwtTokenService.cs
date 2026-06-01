namespace AgroSense.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(
            string userId,
            string email,
            string fullName,
            IEnumerable<string> roles);
    }
}
