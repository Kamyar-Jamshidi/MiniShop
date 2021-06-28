namespace MiniShop.Core.DTO.Response
{
    public class GetAllUsersReponse
    {
        public GetAllUsersReponse(string token, string firstName, string lastName, string username, bool isApproved)
        {
            Token = token;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            IsApproved = isApproved;
        }

        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public bool IsApproved { get; set; }
    }
}
