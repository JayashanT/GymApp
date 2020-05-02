using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Entity
{
    public class Member:User
    {
        public string Name { get; set; }
        public DateTime JoinedDate { get; set; }
        public string MembershipState { get; set; }
        public int MembershipTypeID { get; set; }
        [ForeignKey("MembershipTypeId")]
        public MembershipType MembershipType { get; set; }
    }
}
