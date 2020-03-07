using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarPool.Services.Contracts;
using CarPool.Data.Models;
using System.Collections.Generic;
using AutoMapper;
using CarPool.Application.Models;

namespace CodeFirst.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    public class OfferController:Controller
    {
        private readonly IMapper _mapper;
        private readonly IOfferService _repos;
        public OfferController(IOfferService repos,IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody]Offer Model)
        {
            var offer = _mapper.Map<OfferDBO>(Model);
            offer = _repos.Add(offer);
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
            List<Offer> Offers = new List<Offer>();
            foreach (var offer in _repos.GetAll())
            {
                Offers.Add(_mapper.Map<Offer>(offer));
            }
            return Offers;
        }

        [Route("{id}")]
        [HttpGet]
        public Offer GetByID(int id)
        {
            return _mapper.Map<Offer>(_repos.GetByID(id));
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
            List<Offer> Offers = new List<Offer>();
            foreach(var offer in _repos.FilterOffer(source, destination, seats))
            {
                Offers.Add(_mapper.Map<Offer>(offer));
            }
            return Offers;
        }
    }
}
