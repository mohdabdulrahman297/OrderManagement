namespace AuthService.Models
{
    public class UserLoginResponseViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}