using Newtonsoft.Json;

namespace AuthentikSharp.Rest;

public class AuthentikRestError
{
    [JsonProperty("detail")]
    public string Error { get; set; }
}
