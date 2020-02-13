using CarPoolApplication.Models;
using CodeFirst.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeFirst.Controllers
{
    [Route ("api/[Controller]")]
    public class BookingController:Controller
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
            _repos.Add(booking);
            return "Ok";
        }

        [Route("{id}")]
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
        [HttpPost]
        public void Cancel(int id)
        {
            _repos.UpdateStatus(id, StatusOfRide.Cancelled);
        }
        [Route("Confirm/{id:int}")]
        [HttpPost]
        public void Confirm(int id)
        {
            _repos.UpdateStatus(id, StatusOfRide.Accepted);
        }
        [Route("Reject/{id:int}")]
        [HttpPost]
        public void Reject(int id)
        {
            _repos.UpdateStatus(id, StatusOfRide.Rejected);
        }
        [Route("Complete/{id:int}")]
        [HttpPost]
        public void Complete(int id)
        {
            _repos.UpdateStatus(id, StatusOfRide.Completed);
        }
        [Route("Requests/{id:int}")]
        [HttpGet]
        public List<Booking> GetAll(int id)
        {
            return _repos.Requests(id);
        }
        [Route("Offer/{id:int}")]
        [HttpGet]
        public IList<Booking> OfferID(int id)
        {
           return _repos.GetByOfferID(id);
        }

        
    }
}
