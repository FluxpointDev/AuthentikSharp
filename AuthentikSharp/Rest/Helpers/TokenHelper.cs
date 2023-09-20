using AuthentikSharp.Rest;

namespace AuthentikSharp
{
    public static class TokenHelper
    {

        public static async Task<Pagination<Token>> GetTokensAsync(this AuthentikClient client)
        {
            Pagination<Token>? Users = await client.Rest.GetAsync<Pagination<Token>>("/core/tokens");
            if (Users == null)
                return new Pagination<Token>();

            foreach (var i in Users.Results)
            {
                i.Client = client;

                if (i.User != null)
                    i.User.Client = client;
            }

            return Users;
        }
    }
}
