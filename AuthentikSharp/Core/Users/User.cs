using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AuthentikSharp;

public class User : Entity
{
    [JsonProperty("pk")]
    public long Id { get; set; }

    [JsonProperty("uid")]
    public string Uid { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("is_active")]
    public bool IsActive { get; set; }

    [JsonProperty("last_login", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime? LastLogin { get; set; }

    [JsonProperty("is_superuser")]
    public bool IsSuperUser { get; set; }

    [JsonProperty("groups_obj")]
    public GroupInfo[] Groups = Array.Empty<GroupInfo>();

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("avatar")]
    public string Avatar { get; set; }

    [JsonProperty("attributes")]
    public Dictionary<string, object> Attributes = new Dictionary<string, object>();

    [JsonProperty("path")]
    public string Path;

    [JsonProperty("type")]
    public string Type;
}
