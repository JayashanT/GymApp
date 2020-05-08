using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Entity
{
    public class Gym
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AllowedMembers { get; set; }
        public string ContactNumber { get; set; }
        public DateTime JoinedDate { get; set; }
        public short MembershipDuration { get; set; }
        public string Logo { get; set; }
    }
}
