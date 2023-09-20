using System.Threading.Tasks;

namespace AuthentikSharp;

public static class TenantHelper
{

    public static async Task<Tenant?> GetTenantAsync(this AuthentikClient client, long id)
    {
        Tenant? Link = await client.Rest.GetAsync<Tenant>("/core/tenants/" + id);
        if (Link == null)
            return null;
        Link.Client = client;

        return Link;
    }

    public static async Task<Pagination<Tenant>> GetGroupsAsync(this AuthentikClient client)
    {
        Pagination<Tenant>? Users = await client.Rest.GetAsync<Pagination<Tenant>>("/core/tenants");
        if (Users == null)
            return new Pagination<Tenant>();

        foreach (var i in Users.Results)
        {
            i.Client = client;
        }

        return Users;
    }
}
