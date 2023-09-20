using Newtonsoft.Json;

namespace AuthentikSharp;

public class Link : Entity
{
    [JsonProperty("link")]
    public string LinkURL { get; set; }
}
