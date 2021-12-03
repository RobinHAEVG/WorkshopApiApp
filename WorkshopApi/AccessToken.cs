using System;
using System.Text.Json.Serialization;

namespace WorkshopApi
{
    public class AccessToken
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; }
        [JsonPropertyName("validity")]
        public DateTime ValidUntil { get; set; }
    }
}
