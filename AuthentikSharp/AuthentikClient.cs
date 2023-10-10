using AuthentikSharp.Rest;
using System;

namespace AuthentikSharp;

public class AuthentikClient
{
    public AuthentikClient(string hostUrl, string token)
    {
        if (string.IsNullOrEmpty(hostUrl))
            throw new AuthentikException("Client config ApiUrl can not be empty.");

        if (!Uri.IsWellFormedUriString(hostUrl, UriKind.Absolute))
            throw new AuthentikException("Client config ApiUrl is an invalid format.");

        if (!hostUrl.EndsWith('/'))
            hostUrl += "/";

        string Url = hostUrl + "api/v3/";

        Rest = new AuthentikRestClient(Url, token);
    }

    public AuthentikRestClient Rest { get; internal set; }

    private bool LogRequests;
    private bool LogResponse;

}
