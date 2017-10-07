using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrienXone.Models
{
    public class ApplicationUser
    {
        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName= "accessories")]
        public string[] Accessories { get; set; }

        public string Language { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName="hobbies")]
        public IEnumerable<string> Hobbies { get; set; }

        [JsonProperty(PropertyName = "faceattributes")]
        public FaceAttributes FaceAttributes { get; set; }

        [JsonProperty(PropertyName = "location")]
        public double Location { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
