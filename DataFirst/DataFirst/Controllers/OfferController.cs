using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarPool.Services.Contracts;
using System.Collections.Generic;
using AutoMapper;
using CarPool.Application.Models;

namespace CodeFirst.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    public class OfferController:Controller
    {
  
        private readonly IOfferService _repos;
        public OfferController(IOfferService repos)
        {
            _repos = repos;
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody]Offer Model)
        {
            
            var offer = _repos.Add(Model);
            if (offer == null)
            {
                return BadRequest(new { message = "Error Occured" });
            }
            return Ok(new { message = offer.ID });

        }

        [Route("delete/{id:int}")]
        [HttpPut]
        public string Delete(int id)
        {
            return _repos.Delete(id);
        }

        [Authorize(Roles = "Admin")]
        [Route( "GetAll")]
        [HttpGet]
        public List<Offer> GetAll()
        {
            return _repos.GetAll(); ;
        }

        [Route("{id}")]
        [HttpGet]
        public Offer GetByID(int id)
        {
            return _repos.GetByID(id);
        }
        [Route("Cancel/{id}")]
        [HttpGet]
        public string Cancel(int id)
        {       
                return _repos.UpdateStatus(id, CarPool.Data.Models.StatusOfRide.Cancelled);            
        }

        [Route("Cancel/{id}")]
        [HttpGet]
        public string Complete(int id)
        {
           return _repos.UpdateStatus(id, CarPool.Data.Models.StatusOfRide.Completed);
           
        }

        [Route("DriverOffer/{id:int}")]
        [HttpGet]
        public List<Offer> FilteredOffers(int id)
        {

            return _repos.GetByDriver(id) ;
        }

        [Route("Search")]
        [HttpGet]
        public List<Offer> FilteredOffers([FromQuery] CarPool.Data.Models.Cities source, CarPool.Data.Models.Cities destination,int seats)
        {
            return _repos.FilterOffer(source, destination, seats);
        }
    }
}
