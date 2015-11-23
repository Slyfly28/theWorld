using Microsoft.AspNet.Mvc;
using System;
using System.Linq;
using Microsoft.AspNet.Authorization;
using theWorld.Models;
using theWorld.ViewModels;
using TheWorld.Services;

namespace theWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private ITheWorldRepository _repository;

        public AppController(IMailService service, ITheWorldRepository repository)
        {
            _mailService = service;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            var trips = _repository.GetAllTrips();
            return View(trips);
        }

        public IActionResult About()
        {
           
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid) // check the validity of the mail sent
            {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];

                // additional checkup on the client side
                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("","Could not send email, configuration problem.");
                }

                if (_mailService.SendMail(email,
                    email,
                    $"Contact Page from {model.Name} ({model.Email})",
                    model.Message))
                {
                    ModelState.Clear(); // prevents the multiple send cluster

                    ViewBag.Message = "Mail Sent. Thanks!";
                }
            }

            return View();
        }
    }

}
