using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuthentikSharp;

public class Tenant : Entity
{
    [JsonProperty("tenant_uuid")]
    public string Uuid { get; set; }

    [JsonProperty("domain")]
    public string Domain { get; set; }

    [JsonProperty("default")]
    public bool IsDefault { get; set; }

    [JsonProperty("branding_title")]
    public string BrandingTitle { get; set; }

    [JsonProperty("branding_logo")]
    public string BrandingLogo { get; set; }

    [JsonProperty("branding_favicon")]
    public string BrandingFavicon { get; set; }

    [JsonProperty("flow_authentication")]
    public string FlowAuthenticationUuid { get; set; }

    [JsonProperty("flow_invalidation")]
    public string FlowInvalidationUuid { get; set; }

    [JsonProperty("flow_recovery")]
    public string FlowRecoveryUuid { get; set; }

    [JsonProperty("flow_unenrollment")]
    public string FlowUnenrollmentUuid { get; set; }

    [JsonProperty("flow_user_settings")]
    public string FlowUserSettingsUuid { get; set; }

    [JsonProperty("flow_device_code")]
    public string FlowDeviceCodeUuid { get; set; }

    [JsonProperty("event_retention")]
    public string EventRetentionTime { get; set; }

    [JsonProperty("attributes")]
    public Dictionary<string, object> Attributes = new Dictionary<string, object>();
}
