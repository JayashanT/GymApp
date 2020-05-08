using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.ViewModels
{
    public class MemberVM
    {
        public int Id { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public DateTime JoinedDate { get; set; }
        public string MembershipState { get; set; }
        public int MembershipTypeId { get; set; }
    }
}
