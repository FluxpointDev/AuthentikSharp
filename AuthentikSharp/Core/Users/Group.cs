using Newtonsoft.Json;

namespace AuthentikSharp
{
    public class GroupInfo : Entity
    {
        [JsonProperty("pk")]
        public string Uuid { get; set; }

        [JsonProperty("num_pk")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_superuser")]
        public bool IsSuperUser { get; set; }

        [JsonProperty("parent")]
        public string ParentGroupUuid { get; set; }

        [JsonProperty("parent_name")]
        public string ParentGroupName { get; set; }

        [JsonProperty("attributes")]
        public Dictionary<string, object> Attributes = new Dictionary<string, object>();
    }

    public class Group : Entity
    {
        [JsonProperty("users_obj")]
        public IReadOnlyCollection<User> Users = Array.Empty<User>();
    }
}
