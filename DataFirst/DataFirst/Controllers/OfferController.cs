using CarPoolApplication;
using CarPoolApplication.Models;
using CodeFirst.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeFirst.Controllers
{
    [Route("api/[Controller]")]
    [Authorize]
    public class OfferController:Controller
    {        
        private readonly IOfferService _repos;
        public OfferController(IOfferService repos)
        {
            _repos = repos;
        }

        [Route("Create")]
        [HttpPost]
        public string Create([FromBody]Offer offer)
        {
            return _repos.Add(offer).Response.ReasonPhrase;
        }
        [Route( "GetAll")]
        [HttpGet]
        public List<Offer> GetAll()
        {
            return _repos.GetAll();
        }

        [Route("{id:int}")]
        [HttpGet]
        public Offer GetByID(int id)
        {
            return _repos.GetByID(id);
        }
        [Route("Cancel/{id:int}")]
        [HttpGet]
        public string Cancel(int id)
        {       
                return _repos.UpdateStatus(id,StatusOfRide.Cancelled).Response.ReasonPhrase;            
        }

        [Route("Cancel/{id:int}")]
        [HttpGet]
        public string Complete(int id)
        {
           return _repos.UpdateStatus(id, StatusOfRide.Completed).Response.ReasonPhrase;
           
        }

        [Route("Search")]
        [HttpGet]
        public List<Offer> FilteredOffers([FromQuery] Cities source,Cities destination,int seats)
        {
            return _repos.FilterOffer(source,destination,seats);
        }
    }
}
