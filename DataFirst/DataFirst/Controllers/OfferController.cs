using CarPoolApplication;
using CarPoolApplication.Concerns;
using CodeFirst.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeFirst.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(Roles =Role.User)]
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
            return _repos.GetAll();
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
                return _repos.UpdateStatus(id,StatusOfRide.Cancelled);            
        }

        [Route("Cancel/{id}")]
        [HttpGet]
        public string Complete(int id)
        {
           return _repos.UpdateStatus(id, StatusOfRide.Completed);
           
        }

        [Route("Search")]
        [HttpGet]
        public List<Offer> FilteredOffers([FromQuery] Cities source,Cities destination,int seats)
        {
            return _repos.FilterOffer(source,destination,seats);
        }
    }
}
