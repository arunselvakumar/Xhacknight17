using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrienXone.Models
{
    public class FaceAttributes
    {
        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "age")]
        public float Age { get; set; }

        [JsonProperty(PropertyName = "smile")]
        public string Smile { get; set; }

        [JsonProperty(PropertyName = "accessories")]
        public string[] Accessories{ get; set; }

        [JsonProperty(PropertyName = "emotions")]
        public Emotions Emotions { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
