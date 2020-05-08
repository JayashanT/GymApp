using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string MobileNumber { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public int GymId { get; set; }
        [ForeignKey("GymId")]
        public Gym Gym { get; set; }
    }
}
