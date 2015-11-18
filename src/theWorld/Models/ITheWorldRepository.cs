using System.Collections.Generic;

namespace theWorld.Models
{
    public interface ITheWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
    }
}