using AuthentikSharp.Rest;

namespace AuthentikSharp
{
    public static class AuthenticatorHelper
    {
        public static async Task<Pagination<Authenticator>> GetAuthenticatorsAsync(this AuthentikClient client)
        {
            Pagination<Authenticator>? Users = await client.Rest.GetAsync<Pagination<Authenticator>>("/authenticators/all");
            if (Users == null)
                return new Pagination<Authenticator>();

            foreach (var i in Users.Results)
            {
                i.Client = client;
            }

            return Users;
        }

        public static async Task<StaticAuthenticator> GetStaticAuthenticatorAsync(this AuthentikClient client, long id)
        {
            StaticAuthenticator? Users = await client.Rest.GetAsync<StaticAuthenticator> ("/authenticators/admin/static/" + id);
            if (Users == null)
                return null;
            Users.Client = client;

            return Users;
        }

        public static Task<HttpResponseMessage> DeleteAsync(this Authenticator auth)
            => auth.Client.Rest.DeleteAsync("/authenticators/admin/" + AuthenticatorType(auth) + "/" + auth.Id);

        internal static string AuthenticatorType(Authenticator auth)
        {
            switch (auth.TypeName.ToLower())
            {
                case "webauthn device":
                    return "webauthn";
                case "static device":
                    return "static";
                case "totp device":
                    return "totp";
                case "duo device":
                    return "duo";
            }
            return "sms";
        }

        public static async Task<Pagination<Authenticator>> GetUserAuthenticatorsAsync(this AuthentikClient client, long id)
        {
            Pagination<Authenticator>? Users = await client.Rest.GetAsync<Pagination<Authenticator>>("/authenticators/all/?pk=" + id);
            if (Users == null)
                return new Pagination<Authenticator>();

            foreach (var i in Users.Results)
            {
                i.Client = client;
            }

            return Users;
        }
    }
}
