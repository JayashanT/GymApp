using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Entity
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public List<Image> Images { get; set; }
    }
}
