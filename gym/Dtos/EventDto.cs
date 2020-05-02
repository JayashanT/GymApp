using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }
        public short NoOfSet { get; set; }
        public short Repition { get; set; }
        public short BreakTime { get; set; }
        public int SheduleId { get; set; }
        public int ExerciseId { get; set; }
    }
}
