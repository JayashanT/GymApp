using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public short MobileNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
