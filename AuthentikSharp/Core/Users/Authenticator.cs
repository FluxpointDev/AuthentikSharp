using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthentikSharp
{
    public class Authenticator : Entity
    {
        [JsonProperty("pk")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("verbose_name")]
        public string TypeName { get; set; }

        [JsonProperty("confirmed")]
        public bool IsConfirmed { get; set; }
    }
    public class StaticAuthenticator : Entity
    {
        [JsonProperty("token_set")]
        public IReadOnlyCollection<StaticAuthenticatorCode> Codes { get; set; }
    }
    public class StaticAuthenticatorCode
    {
        [JsonProperty("token")]
        public string Code { get; set; }
    }
}
