namespace AuthService.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;

        // Navigation property
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}