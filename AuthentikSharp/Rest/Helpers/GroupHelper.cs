using AuthentikSharp.Rest;

namespace AuthentikSharp
{
    public static class GroupHelper
    {
        public static async Task<Group?> GetGroupAsync(this AuthentikClient client, long id)
        {
            Group? User = await client.Rest.GetAsync<Group>("/core/groups/" + id);
            if (User == null)
                return null;
            User.Client = client;
            foreach (var i in User.Users)
            {
                i.Client = client;
                foreach (var g in i.Groups)
                {
                    g.Client = client;
                }
            }
            return User;
        }

        public static async Task<Pagination<GroupInfo>> GetUserGroupsAsync(this AuthentikClient client, long id)
        {
            Pagination<GroupInfo>? Users = await client.Rest.GetAsync<Pagination<GroupInfo>>("/core/groups/?members_by_pk=" + id);
            if (Users == null)
                return new Pagination<GroupInfo>();

            foreach (var i in Users.Results)
            {
                i.Client = client;
            }

            return Users;
        }

        public static async Task<Pagination<Group>> GetGroupsAsync(this AuthentikClient client)
        {
            Pagination<Group>? Users = await client.Rest.GetAsync<Pagination<Group>>("/core/groups");
            if (Users == null)
                return new Pagination<Group>();

            foreach (var i in Users.Results)
            {
                i.Client = client;
                foreach (var u in i.Users)
                {
                    u.Client = client;
                }
            }

            return Users;
        }

    }
}
