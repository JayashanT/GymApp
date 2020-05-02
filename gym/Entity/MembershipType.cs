using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Entity
{
    public class MembershipType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Fee{ get; set; }
        public int DurationInMonths { get; set; }
        public int GymId { get; set; }
        [ForeignKey("GymId")]
        public Gym Gym { get; set; }
    }
}
