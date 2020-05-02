using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Entity
{
    public class Shedule
    {
        public int Id { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string Name { get; set; }

        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

        public virtual List<Event> Events { get; set; }

    }
}
