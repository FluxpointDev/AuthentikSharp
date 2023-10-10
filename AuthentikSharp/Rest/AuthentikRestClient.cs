using HttpCoreSharp;
using System;
using System.Net.Http;

namespace AuthentikSharp.Rest;

public class AuthentikRestClient : HttpCoreClient<IAuthentikRequest, IHttpCoreError>
{
    internal static HttpClient BuildHttp(string hostUrl, string token)
    {
        HttpClient Http = new HttpClient
        {
            BaseAddress = new Uri(hostUrl)
        };

        Http.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        Http.DefaultRequestHeaders.Add("User-Agent", "AuthentikSharp");
        Http.DefaultRequestHeaders.Add("Accept", "*/*");
        return Http;
    }

    internal AuthentikRestClient(string hostUrl, string token) : base(hostUrl, new HttpCoreOptions
    {
        EndpointRequiresEndingSlash = true,
        Http = BuildHttp(hostUrl, token)
    })
    {

    }
}
