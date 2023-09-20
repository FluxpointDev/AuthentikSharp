using System.Threading.Tasks;

namespace AuthentikSharp;

internal static class _Helper
{

    public static async Task<Link?> GetRecoveryLinkAsync(this AuthentikClient client, long id)
    {
        Link? Link = await client.Rest.GetAsync<Link>("/core/users/" + id + "/recovery");
        if (Link == null)
            return null;
        Link.Client = client;

        return Link;
    }
}
