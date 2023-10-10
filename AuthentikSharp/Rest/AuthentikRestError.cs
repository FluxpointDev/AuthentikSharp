using HttpCoreSharp;
using Newtonsoft.Json;

namespace AuthentikSharp.Rest;

public class AuthentikRestError : IHttpCoreError
{
    [JsonProperty("detail")]
    public override string ErrorMessage { get; set; }
}
