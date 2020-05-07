using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Dtos
{
    public class EventRecordDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Weight { get; set; }
        public int EventId { get; set; }
    }
}
