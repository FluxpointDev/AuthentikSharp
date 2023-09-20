using AuthentikSharp.Rest;

namespace AuthentikSharp
{
    public class AuthentikClient
    {
        public AuthentikClient(string hostUrl, string token)
        {
            Rest = new AuthentikRestClient(this, hostUrl, token);
        }

        public AuthentikRestClient Rest { get; internal set; }

        public bool LogRequests;
        public bool LogResponse;

    }
}
