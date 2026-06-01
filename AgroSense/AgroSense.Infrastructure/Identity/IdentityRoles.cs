namespace AgroSense.Infrastructure.Identity
{
    public static class IdentityRoles
    {
        public const string Admin = "Admin";
        public const string Operator = "Operator";
        public const string Viewer = "Viewer";

        public static readonly string[] All =
        {
            Admin,
            Operator,
            Viewer
        };
    }

}
