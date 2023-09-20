using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthentikSharp
{
    public class SystemInfo : Entity
    {
        [JsonProperty("runtime")]
        public SystemInfoRuntime Runtime { get; set; }

        [JsonProperty("tenant")]
        public string DefaultTenantName {  get; set; }

        [JsonProperty("server_time")]
        public DateTime ServerTime { get; set; }
    }
    public class SystemInfoRuntime
    {
        [JsonProperty("python_version")]
        public string PythonVersion { get; set; }

        [JsonProperty("gunicorn_version")]
        public string GunicornVersion { get; set; }

        [JsonProperty("environment")]
        public string Environment { get; set; }

        [JsonProperty("architechture")]
        public string Architechture { get; set; }

        [JsonProperty("uname")]
        public string SystemName { get; set; }
    }
}
