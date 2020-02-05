using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BlazorAspnetHostedOidcJS.Shared.Models
{
    public class OidcUserProfile
    {
        [JsonPropertyName("amr")]
        public List<string> Amr { get; set; }

        [JsonPropertyName("auth_time")]
        public int AuthTime { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }        
        [JsonPropertyName("email_verified")]
        public bool EmailVerified { get; set; }         
        [JsonPropertyName("family_name")]
        public string FamilyName { get; set; }          
        [JsonPropertyName("given_name")]
        public string GivenName { get; set; }
        [JsonPropertyName("idp")]
        public string Idp { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("s_hash")]
        public string Shash { get; set; }

        [JsonPropertyName("sid")]
        public string Sid { get; set; }

        [JsonPropertyName("sub")]
        public string Sub { get; set; }

        [JsonPropertyName("website")]
        public string Website { get; set; }
    }
}
