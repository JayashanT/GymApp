using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Dtos
{
    public class MembershipTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Fee { get; set; }
        public int DurationInMonths { get; set; }
        public int GymId { get; set; }
    }
}
