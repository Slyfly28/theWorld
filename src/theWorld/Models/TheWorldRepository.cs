﻿using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Logging;

namespace theWorld.Models
{
    public class TheWorldRepository : ITheWorldRepository
    {
        private WorldContext _context;
        private ILogger<TheWorldRepository> _logger;

        public TheWorldRepository(WorldContext context, ILogger<TheWorldRepository> logger )
        {
            _logger = logger;
            _context = context;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return _context.Trips.OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                
               _logger.LogError("Could not get trips from database", ex);
                return null;
            }
           
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return _context.Trips
                .Include(t => t.Stops)
                .OrderBy(t => t.Name)
                .ToList();
            }
            catch (Exception ex)
            {


                _logger.LogError("Could not get trips with stops from database", ex);
                return null;
            }
            
        } 
    }
}
