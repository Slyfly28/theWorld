using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace theWorld.Models
{
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}