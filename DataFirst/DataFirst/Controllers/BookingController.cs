using CarPoolApplication.Models;
using CodeFirst.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeFirst.Controllers
{
    [Route ("Booking")]
    public class BookingController:ControllerBase
    {
        private IBookingService _repos;
        public BookingController(IBookingService repos)
        {
            _repos = repos;
        }

        [Route("Create")]
        [HttpPost]
        public string Create([FromBody]Booking booking)
        {
            if (!ModelState.IsValid)
                return "Bad";
            _repos.Add(_repos.Create(booking));
            return "Ok";
        }

        [Route("{id:int}")]
        [HttpGet]
        public Booking GetByID(int id)
        {
            return _repos.GetByID(id);
        }

        [Route("Rider/{id:int}")]
        [HttpGet]
        public IList<Booking> GetUserBookings(int id)
        {
            return _repos.GetByRiderID(id) ;
        }

        [Route("Delete/{id:int}")]
        [HttpGet]
        public void Delete(int id)
        {
            _repos.Delete(id);
        }

        [Route("Cancel/{id:int}")]
        [HttpGet]
        public void Cancel(int id)
        {
            _repos.UpdateStatus(id, StatusOfRide.Cancelled);
        }
        [Route("Confirm/{id:int}")]
        [HttpGet]
        public void Confirm(int id)
        {
            _repos.UpdateStatus(id, StatusOfRide.Accepted);
        }
        [Route("Reject/{id:int}")]
        [HttpGet]
        public void Reject(int id)
        {
            _repos.UpdateStatus(id, StatusOfRide.Rejected);
        }
        [Route("Complete/{id:int}")]
        [HttpGet]
        public void Complete(int id)
        {
            _repos.UpdateStatus(id, StatusOfRide.Completed);
        }
        [Route("Offer/{id:int}")]
        [HttpGet]
        public IList<Booking> OfferID(int id)
        {
           return _repos.GetByOfferID(id);
        }

        
    }
}
