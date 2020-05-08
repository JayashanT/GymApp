using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Dtos
{
    public class AdminDto
    {
        public int Id { get; set; }
        public string MobileNumber { get; set; }
        public string Token { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
    }
}
