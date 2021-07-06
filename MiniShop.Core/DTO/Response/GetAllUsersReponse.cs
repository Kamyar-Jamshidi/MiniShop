namespace MiniShop.Core.DTO.Response
{
    public class GetAllUsersReponse
    {
        public GetAllUsersReponse(string id, string firstName, string lastName, string username, bool isApproved)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            IsApproved = isApproved;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public bool IsApproved { get; set; }
    }
}
