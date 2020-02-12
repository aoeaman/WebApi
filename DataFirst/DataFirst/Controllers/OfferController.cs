using CarPoolApplication.Models;
using CodeFirst.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeFirst.Controllers
{
    [Route("Offer")]
    public class OfferController:ControllerBase
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
            if (!ModelState.IsValid)
                return "Bad";
            _repos.Add(_repos.Create(offer));
            return "Ok";
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
            if (!ModelState.IsValid)
                return "Bad";
            _repos.Cancel(id);
            return "Ok";
        }

        [Route("Requests/{id:int}")]
        [HttpGet]
        public List<Offer> GetAll(int id)
        {
            return _repos.Requests(id);
        }

        [Route("Search")]
        [HttpGet]
        public List<Offer> FilteredOffers([FromQuery] Cities source,Cities destination,int seats)
        {
            return _repos.FilterOffer(source,destination,seats);
        }
    }
}
