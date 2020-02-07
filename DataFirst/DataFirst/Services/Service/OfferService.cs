﻿using System;
using System.Collections.Generic;
using System.IO;
using CarPoolApplication.Models;
using Newtonsoft.Json;
using CarPoolApplication.Services.Interfaces;
using CodeFirst.Models;
using System.Linq;

namespace CarPoolApplication.Services
{
    public class OfferService:IOfferService
    {
        UtilityService Service;
        Context _context;
        public OfferService(Context context)
        {
            Service = new UtilityService();
            _context = context;
        }

        public void Add(Offer offer)
        {
            _context.Offers.Add(offer);
        }

        
        public Offer Create(Offer Offer)
        {
            Offer.ID = Service.GenerateID();
            return Offer;
        }

        public List<Offer> GetAll()
        {
            return _context.Offers.ToList() ;
        }

        public void Delete(int iD)
        {
            _context.Offers.Remove(_context.Offers.Find(iD));
        }

        public Offer GetByID(int iD)
        {
            return _context.Offers.Find(iD);
        }

        public Offer Update(Offer Offer)
        {
            throw new NotImplementedException();
        }
    }
}
