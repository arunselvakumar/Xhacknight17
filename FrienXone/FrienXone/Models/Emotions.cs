using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrienXone.Models
{
    public class Emotions
    {
        [JsonProperty(PropertyName = "anger")]
        public double Anger { get; set; }

        [JsonProperty(PropertyName = "contempt")]
        public double Contempt { get; set; }

        [JsonProperty(PropertyName = "disgust")]
        public double Disgust { get; set; }

        [JsonProperty(PropertyName = "fear")]
        public double Fear { get; set; }

        [JsonProperty(PropertyName = "happiness")]
        public double Happiness { get; set; }

        [JsonProperty(PropertyName = "neutral")]
        public double Neutral { get; set; }

        [JsonProperty(PropertyName = "sadness")]
        public double Saddness { get; set; }

        [JsonProperty(PropertyName = "surprise")]
        public double Surprise { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
