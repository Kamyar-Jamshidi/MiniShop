namespace MiniShop.Core.DTO.Response
{
    public class LoginResponse
    {
        public LoginResponse(string token, string firstName, string lastName, bool isSuperAdmin)
        {
            Token = token;
            FirstName = firstName;
            LastName = lastName;
            IsSuperAdmin = isSuperAdmin;
        }

        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}
