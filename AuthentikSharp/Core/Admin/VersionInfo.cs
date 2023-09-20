using Newtonsoft.Json;

namespace AuthentikSharp;

public class VersionInfo : Entity
{
    [JsonProperty("version_current")]
    public string CurrentVersion { get; set; }

    [JsonProperty("version_latest")]
    public string LatestVersion {  get; set; }

    [JsonProperty("build_hash")]
    public string BuildHash { get; set; }

    [JsonProperty("outdated")]
    public bool IsUpdateAvailable { get; set; }
}
