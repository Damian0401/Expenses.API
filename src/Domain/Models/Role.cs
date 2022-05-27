namespace Domain.Models
{
    public class Role
    {
        public static List<string> AllRoles = new()
        {
            Administrator,
            Tenant
        };
        public const string Administrator = "Administrator";
        public const string Tenant = "Tenant";
    }
}