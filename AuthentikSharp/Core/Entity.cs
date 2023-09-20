using Newtonsoft.Json;

namespace AuthentikSharp;

public abstract class Entity
{
    [JsonIgnore]
    internal AuthentikClient Client { get; set; }

}
