using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrienXone.Models
{
    public class EntityModel
    {
        public string Entity { get; set; }

        public string Type { get; set; }

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public double Score { get; set; }
    }
}
