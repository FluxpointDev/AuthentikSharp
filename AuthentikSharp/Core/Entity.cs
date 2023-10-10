using Newtonsoft.Json;

namespace AuthentikSharp;

public class Entity
{
    [JsonIgnore]
    public AuthentikClient Client { get; set; }
}
