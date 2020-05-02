using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Entity
{
    public class Event
    {
        public int Id { get; set; }
        public short NoOfSet { get; set; }
        public short Repition { get; set; }
        public short BreakTime { get; set; }
        public int SheduleId { get; set; }
        public int ExerciseId { get; set; }
        [ForeignKey("SheduleId")]
        public Shedule Shedule { get; set; }
        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }
    }
}
