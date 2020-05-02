using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Entity
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {

        }
        DbSet<User> User { get; set; }
        DbSet<Member> Memeber { get; set; }
        DbSet<Admin> Admin { get; set; }
        DbSet<Gym> Gym { get; set; }
        DbSet<Exercise> Exercise { get; set; }
        DbSet<Event> Event { get; set; }
        DbSet<Shedule> Shedule { get; set; }
    }
}
