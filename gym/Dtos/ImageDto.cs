using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Dtos
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int ExerciseId { get; set; }
    }
}
