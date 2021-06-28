namespace MiniShop.Core.DTO.Request
{
    public class ApproveUserRequest
    {
        public ApproveUserRequest(string token, string userToken)
        {
            Token = token;
            UserToken = userToken;
        }

        public string Token { get; set; }
        public string UserToken { get; set; }
    }
}
