using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;
using theWorld.Models;
using theWorld.ViewModels;

namespace theWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripController : Controller
    {
        private ILogger<TripController> _logger;
        private ITheWorldRepository _repository;

        public TripController(ITheWorldRepository repository, ILogger<TripController> logger )
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet("")] // specifying the route
        public JsonResult Get() // Restful database call. return it as in JSON Format
        {
            var results = Mapper.Map<IEnumerable<TripViewModel>>(_repository.GetAllTripsWithStops());
            return Json(results);
        }

        [HttpPost("")] // specifying the route
        public JsonResult Post([FromBody]TripViewModel vm) // allows mapping of JSON types to .Net Types from the body
        {
            if (ModelState.IsValid) // check to see if API is receiving valid data
            {
                try
                {
                    var newTrip = Mapper.Map<Trip>(vm);

                    //Save to the database
                    _logger.LogInformation("Attempting to save a new trip");
                    _repository.AddTrip(newTrip);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(newTrip));
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to save new trip", ex);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Message = ex.Message });

                }
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new {Message = "Failed", ModelState = ModelState});

        }
    }
}
