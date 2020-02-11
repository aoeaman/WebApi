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
        public void Create(Booking booking)
        {
            _repos.Add(_repos.Create(booking));
        }

        [Route("GetByID/{id:int}")]
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
            _repos.Cancel(id);
        }

        [Route("Offer/{id:int}")]
        [HttpGet]
        public IList<Booking> OfferID(int id)
        {
           return _repos.GetByOfferID(id);
        }
    }
}
