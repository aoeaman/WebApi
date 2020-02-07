using CarPoolApplication.Models;
using CarPoolApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Controllers
{
    [Route("Offer")]
    public class OfferController
    {
        private IOfferService _repos;
        public OfferController(IOfferService repos)
        {
            _repos = repos;
        }
        [Route( "GetAll")]
        [HttpGet]
        public List<Offer> GetAll()
        {
            return _repos.GetAll();
        }
    }
}
