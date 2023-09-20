using AuthentikSharp.Rest;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuthentikSharp;

public static class UserHelper
{
    public static Task<HttpResponseMessage> ChangePasswordAsync(this User user, string password)
        => PostChangePasswordAsync(user.Client, user.Id, password);

    public static async Task<HttpResponseMessage> PostChangePasswordAsync(this AuthentikClient client, long id, string password)
    {
        return await client.Rest.PostAsync($"/core/users/{id}/set_password", new ChangePasswordRequest
        {
            Password = password
        });
    }

    public static Task<HttpResponseMessage> DeleteAsync(this User user)
        => DeleteUserAsync(user.Client, user.Id);

    public static Task<HttpResponseMessage> DeleteUserAsync(this AuthentikClient client, long id)
        => client.Rest.DeleteAsync("/core/users/" + id);

    public static Task<Link?> GetRecoveryLink(this User user)
        => GetRecoveryLinkAsync(user.Client, user.Id);

    public static async Task<Link?> GetRecoveryLinkAsync(this AuthentikClient client, long id)
    {
        Link? Link = await client.Rest.GetAsync<Link>("/core/users/" + id + "/recovery");
        if (Link == null)
            return null;
        Link.Client = client;

        return Link;
    }

    public static async Task<User?> GetUserAsync(this AuthentikClient client, long id)
    {
        User? User = await client.Rest.GetAsync<User>("/core/users/" + id);
        if (User == null)
            return null;
        User.Client = client;

        return User;
    }

    public static async Task<Pagination<User>> GetUsersAsync(this AuthentikClient client)
    {
        Pagination<User>? Users = await client.Rest.GetAsync<Pagination<User>>("/core/users");
        if (Users == null)
            return new Pagination<User>();

        foreach(var i in Users.Results)
        {
            i.Client = client;
            foreach(var g in i.Groups)
            {
                g.Client = client;
            }
        }

        return Users;
    }

    

    public static Task<Pagination<Session>> GetSessionsAsync(this User user)
        => GetUserSessionsAsync(user.Client, user.Id);

    public static async Task<Pagination<Session>> GetUserSessionsAsync(this AuthentikClient client, long id)
    {
        Pagination<Session>? Users = await client.Rest.GetAsync<Pagination<Session>>("/core/authenticated_sessions/?user=" + id);
        if (Users == null)
            return new Pagination<Session>();

        foreach (var i in Users.Results)
        {
            i.Client = client;
        }

        return Users;
    }

    public static Task<Pagination<Authenticator>> GetAuthenticatorsAsync(this User user)
        => AuthenticatorHelper.GetUserAuthenticatorsAsync(user.Client, user.Id);

    

    
}
