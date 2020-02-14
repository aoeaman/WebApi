using CarPoolApplication.Models;
using CodeFirst.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeFirst.Controllers
{
    [Route ("api/[Controller]")]
    [Authorize]
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
            return _repos.Add(booking).Response.ReasonPhrase;
        }

        [Route("GetAll")]
        [HttpGet]
        public List<Booking> GetAll()
        {
            return _repos.GetAll();
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
        public string Delete(int id)
        {
            return _repos.Delete(id).Response.ReasonPhrase;
        }

        [Route("Cancel/{id:int}")]
        [HttpPost]
        public string Cancel(int id)
        {
            return _repos.UpdateStatus(id, StatusOfRide.Cancelled).Response.ReasonPhrase;
        }
        [Route("Confirm/{id:int}")]
        [HttpPost]
        public string Confirm(int id)
        {
            return _repos.UpdateStatus(id, StatusOfRide.Accepted).Response.ReasonPhrase;
        }
        [Route("Reject/{id:int}")]
        [HttpPost]
        public string Reject(int id)
        {
            return _repos.UpdateStatus(id, StatusOfRide.Rejected).Response.ReasonPhrase;
        }
        [Route("Complete/{id:int}")]
        [HttpPost]
        public string Complete(int id)
        {
            return _repos.UpdateStatus(id, StatusOfRide.Completed).Response.ReasonPhrase;
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
