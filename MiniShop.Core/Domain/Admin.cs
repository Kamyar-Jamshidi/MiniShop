namespace MiniShop.Core.Domain
{
    public class Admin : BaseDomain
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsApproved { get; set; }
        public string Token { get; set; }
    }
}
