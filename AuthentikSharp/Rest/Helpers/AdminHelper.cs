using AuthentikSharp.Rest;

namespace AuthentikSharp
{
    public static class AdminHelper
    {

        public static async Task<SystemInfo?> GetSystemInfoAsync(this AuthentikClient client)
        {
            SystemInfo? Info = await client.Rest.GetAsync<SystemInfo>("/admin/system");
            if (Info == null)
                return null;
            Info.Client = client;

            return Info;
        }

        public static async Task<VersionInfo?> GetVersionAsync(this AuthentikClient client)
        {
            VersionInfo? Info = await client.Rest.GetAsync<VersionInfo>("/admin/version");
            if (Info == null)
                return null;
            Info.Client = client;

            return Info;
        }
    }
}
