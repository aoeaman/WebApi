using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CarPool.Services.Contracts;
using CarPool.Data.Models;
using CarPool.Application.Models;
using AutoMapper;

namespace CodeFirst.Controllers
{
    [Route ("api/[Controller]")]
    [Authorize]
    public class BookingController:Controller
    {
        private readonly IBookingService _repos;
        private readonly IMapper _mapper;
        public BookingController(IBookingService repos,IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody]Booking Model)
        {
            var booking = _repos.Add(_mapper.Map<BookingDBO>(Model));

            if (booking == null)
            {
                return BadRequest(new { message = "Error Occured" });
            }
            return Ok(new { message = booking.ID });
        }

        [Authorize(Roles = Role.Admin)]
        [Route("GetAll")]
        [HttpGet]
        public List<Booking> GetAll()
        {
            List<Booking> Bookings = new List<Booking>();
            foreach(var booking in _repos.GetAll())
            {
                Bookings.Add(_mapper.Map<Booking>(booking));
            }
            return Bookings;
        }

        [Route("{id}")]
        [HttpGet]
        public Booking GetByID(int id)
        {
            return _mapper.Map<Booking>(_repos.GetByID(id));
        }

        [Route("{id:int}/Rider")]
        [HttpGet]
        public IList<Booking> GetUserBookings(int id)
        {
            List<Booking> Bookings = new List<Booking>();
            foreach (var booking in _repos.GetByRiderID(id))
            {
                Bookings.Add(_mapper.Map<Booking>(booking));
            }
            return Bookings;
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
            List<Booking> Bookings = new List<Booking>();
            foreach (var booking in _repos.Requests(id))
            {
                Bookings.Add(_mapper.Map<Booking>(booking));
            }
            return Bookings;
        }

        [Route("{id:int}/Offer")]
        [HttpGet]
        public IList<Booking> OfferID(int id)
        {
            List<Booking> Bookings = new List<Booking>();
            foreach (var booking in _repos.GetByOfferID(id))
            {
                Bookings.Add(_mapper.Map<Booking>(booking));
            }
            return Bookings;
        }       
    }
}
