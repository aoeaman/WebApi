using CarPoolApplication;
using CarPoolApplication.Concerns;
using CodeFirst.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeFirst.Controllers
{
    [Route ("api/[Controller]")]
    [Authorize(Roles =Role.User)]
    public class BookingController:Controller
    {
        private readonly IBookingService _repos;
        public BookingController(IBookingService repos)
        {
            _repos = repos;
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody]Booking Model)
        {
            var booking = _repos.Add(Model);
            if (booking == null)
            {
                return BadRequest(new { message = "Error Occured" });
            }
            return Ok(new { message = "Successfully Created with ID = " + booking.ID });
        }

        [Authorize(Roles = Role.Admin)]
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

        [Route("{id:int}/Rider")]
        [HttpGet]
        public IList<Booking> GetUserBookings(int id)
        {
            return _repos.GetByRiderID(id) ;
        }

        [Route("Delete/{id}")]
        [HttpGet]
        public string Delete(int id)
        {
            return _repos.Delete(id);
        }

        [Route("{id:int}/Cancel")]
        [HttpPost]
        public string Cancel(int id)
        {
            return _repos.UpdateStatus(id, StatusOfRide.Cancelled);
        }

        [Route("{id:int}/Confirm")]
        [HttpPost]
        public string Confirm(int id)
        {
            return _repos.UpdateStatus(id, StatusOfRide.Accepted);
        }

        [Route("{id:int}/Reject")]
        [HttpPost]
        public string Reject(int id)
        {
            return _repos.UpdateStatus(id, StatusOfRide.Rejected);
        }

        [Route("{id:int}/Complete")]
        [HttpPost]
        public string Complete(int id)
        {
            return _repos.UpdateStatus(id, StatusOfRide.Completed);
        }

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
