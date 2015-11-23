using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using theWorld.Models;

namespace TheWorld.Models
{
    public class WorldContextSeedData
    {
        private WorldContext _context;
        private UserManager<WorldUser> _userManager;

        public WorldContextSeedData(WorldContext context, UserManager<WorldUser> userManager )
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync("greg.harlok@theworld.com") == null)
            {
                //Add the user
                var newUser = new WorldUser()
                {
                    UserName = "gharlok",
                    Email = "greg.harlok@theworld.com"
                };

                await _userManager.CreateAsync(newUser, "abcd1234!");
            }
            if (!_context.Trips.Any())
            {
                //Add New Data
                var usTrip = new Trip()
                {
                    Name = "US Trip",
                    Created = DateTime.UtcNow,
                    UserName = "gharlok",
                    Stops = new List <Stop>()
                    {
                        new Stop() {Name = "Atlanta, GA", Arrival = new DateTime(2014, 5, 21), Latitude = 33.7550, Longitude = 84.3900, Order = 0},
                        new Stop() {Name = "Phoenix, AZ", Arrival = new DateTime(2014, 5, 25), Latitude = 33.4500, Longitude = 112.0667, Order = 1},
                        new Stop() {Name = "Los Angeles, CA", Arrival = new DateTime(2014, 6, 10), Latitude = 34.0500, Longitude = 118.2500, Order = 2},
                        new Stop() {Name = "Dallas, TX", Arrival = new DateTime(2014, 6, 25), Latitude = 32.7767, Longitude = 96.7970, Order = 3},
                        new Stop() {Name = "Atlanta, GA", Arrival = new DateTime(2014, 7, 3), Latitude = 33.7550, Longitude = 84.3900, Order = 4}

                    }

                };

                _context.Trips.Add(usTrip);
                _context.Stops.AddRange(usTrip.Stops);
                var worldTrip = new Trip()
                {
                    Name = "World Trip",
                    Created = DateTime.UtcNow,
                    UserName = "gharlok",
                    Stops = new List<Stop>()
                    {
                        new Stop() {Name = "Country 1", Arrival = new DateTime(2015, 4, 21), Latitude = 33.7550, Longitude = 84.3900, Order = 0},
                        new Stop() {Name = "Country 2", Arrival = new DateTime(2015, 4, 25), Latitude = 33.4500, Longitude = 112.0667, Order = 1},
                        new Stop() {Name = "Country 3", Arrival = new DateTime(2015, 5, 10), Latitude = 34.0500, Longitude = 118.2500, Order = 2},
                        new Stop() {Name = "Country 4", Arrival = new DateTime(2015, 5, 25), Latitude = 32.7767, Longitude = 96.7970, Order = 3},
                        new Stop() {Name = "Country 5", Arrival = new DateTime(2015, 6, 3), Latitude = 33.7550, Longitude = 84.3900, Order = 4}
                    }

                };

                _context.Trips.Add(worldTrip);
                _context.Stops.AddRange(worldTrip.Stops);

                _context.SaveChanges();
            }
        }
    }

}
