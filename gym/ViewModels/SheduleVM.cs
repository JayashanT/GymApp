using gym.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.ViewModels
{
    public class SheduleVM
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Name { get; set; }
        public virtual List<Event> Events { get; set; }
    }
}
