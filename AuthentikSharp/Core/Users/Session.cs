using Newtonsoft.Json;
using System;

namespace AuthentikSharp;

public class Session : Entity
{
    [JsonProperty("uuid")]
    public string Uuid { get; set; }

    [JsonProperty("current")]
    public bool IsCurrentSession { get; set; }

    [JsonProperty("expires")]
    public DateTime ExpireDate { get; set; }

    [JsonProperty("last_used")]
    public DateTime LastUsedDate { get; set; }

    [JsonProperty("last_user_agent")]
    public string LastUserAgent { get; set; }

    [JsonProperty("last_ip")]
    public string LastIP { get; set; }

    [JsonProperty("geo_ip")]
    public SessionGeo Geo { get; set; }
}
public class SessionGeo
{
    [JsonProperty("continent")]
    public string Continent { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }
}
