using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthentikSharp
{
    public class Token : Entity
    {
        [JsonProperty("pk")]
        public string Uuid { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("intent")]
        public string Intent { get; set; }

        [JsonProperty("user")]
        public long UserId { get; set; }

        [JsonProperty("user_obj")]
        public User User { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("expires")]
        public DateTime? ExpireDate { get; set; }

        [JsonProperty("expiring")]
        public bool IsExpiringToken { get; set; }
    }
}
