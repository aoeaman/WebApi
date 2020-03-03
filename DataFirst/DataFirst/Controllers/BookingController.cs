using CarPoolApplication;
using CarPoolApplication.Concerns;
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

        [Authorize(Roles = Role.User)]
        [Route("Create")]
        [HttpPost]
        public string Create([FromBody]Booking booking)
        {
            return _repos.Add(booking).Response.ReasonPhrase;
        }

        [Authorize(Roles = Role.Admin)]
        [Route("GetAll")]
        [HttpGet]
        public List<Booking> GetAll()
        {
            return _repos.GetAll();
        }

        [Authorize(Roles = Role.User)]
        [Route("{id}")]
        [HttpGet]
        public Booking GetByID(int id)
        {
            return _repos.GetByID(id);
        }

        [Authorize(Roles = Role.User)]
        [Route("{id:int}/Rider")]
        [HttpGet]
        public IList<Booking> GetUserBookings(int id)
        {
            return _repos.GetByRiderID(id) ;
        }

        [Authorize(Roles = Role.User)]
        [Route("{id:int}/Delete")]
        [HttpGet]
        public string Delete(int id)
        {
            return _repos.Delete(id).Response.ReasonPhrase;
        }

        [Authorize(Roles = Role.User)]
        [Route("{id:int}/Cancel")]
        [HttpPost]
        public string Cancel(int id)
        {
            return _repos.UpdateStatus(id, StatusOfRide.Cancelled).Response.ReasonPhrase;
        }

        [Authorize(Roles = Role.User)]
        [Route("{id:int}/Confirm")]
        [HttpPost]
        public string Confirm(int id)
        {
            return _repos.UpdateStatus(id, StatusOfRide.Accepted).Response.ReasonPhrase;
        }

        [Authorize(Roles = Role.User)]
        [Route("{id:int}/Reject")]
        [HttpPost]
        public string Reject(int id)
        {
            return _repos.UpdateStatus(id, StatusOfRide.Rejected).Response.ReasonPhrase;
        }

        [Authorize(Roles = Role.User)]
        [Route("{id:int}/Complete")]
        [HttpPost]
        public string Complete(int id)
        {
            return _repos.UpdateStatus(id, StatusOfRide.Completed).Response.ReasonPhrase;
        }

        [Authorize(Roles = Role.User)]
        [Route("{id:int}/Requests")]
        [HttpGet]
        public List<Booking> GetAll(int id)
        {
            return _repos.Requests(id);
        }

        [Route("{id:int}/Offer")]
        [HttpGet]
        public IList<Booking> OfferID(int id)
        {
           return _repos.GetByOfferID(id);
        }       
    }
}
