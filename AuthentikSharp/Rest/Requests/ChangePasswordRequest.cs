using Newtonsoft.Json;

namespace AuthentikSharp.Rest
{
    internal class ChangePasswordRequest : IAuthentikRequest
    {
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
