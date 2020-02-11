using CarPoolApplication.Models;
using CodeFirst.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeFirst.Controllers
{
    [Route("Offer")]
    public class OfferController:ControllerBase
    {
        private IOfferService _repos;
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

        [Route("GetByID/{id:int}")]
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
            _repos.Delete(id);
            return "Ok";
        }
    }
}
