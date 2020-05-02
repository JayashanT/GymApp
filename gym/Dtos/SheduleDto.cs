using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Dtos
{
    public class SheduleDto
    {
        public int Id { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public int MemberId { get; set; }
    }
}
