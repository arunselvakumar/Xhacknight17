using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrienXone.Models
{
    public class LuisResponse
    {
        public string Query { get; set; }

        public ScoringIntent TopScoringIntent { get; set; }

        public IEnumerable<ScoringIntent> Intents { get; set; }

        public IEnumerable<EntityModel> Entities { get; set; }
    }
}
