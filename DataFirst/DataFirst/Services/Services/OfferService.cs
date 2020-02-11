using System;
using System.Collections.Generic;
using System.IO;
using CarPoolApplication.Models;
using Newtonsoft.Json;
using CodeFirst.Services.Interfaces;
using CodeFirst.Models;
using System.Linq;

namespace CarPoolApplication.Services
{
    public class OfferService:IOfferService

    {
        readonly UtilityService Service;
        Context _context;
        public OfferService(Context context)
        {
            Service = new UtilityService();
            _context = context;
        }
        public void Add(Offer offer)
        {           
            _context.Offers.Add(offer);
            _context.SaveChanges();
        }       
        public Offer Create(Offer offer)
        {
            offer.ID = Service.GenerateID();
            foreach (var x in offer.ViaPoints.ToList())
            {
                x.OfferID = offer.ID;
            }            
            offer.Status = StatusOfRide.Created;
            offer.CurrentLocaton = offer.Source;
            return offer;
        }
        public List<Offer> GetAll()
        {
            return _context.Offers.ToList() ;
        }
        public void Cancel(int iD)
        {
            _context.Offers.Find(iD).Status = StatusOfRide.Cancelled;
            _context.SaveChanges();
        }
        public Offer GetByID(int iD)
        {
            return _context.Offers.Find(iD);
        }
        public Offer Update(Offer Offer)
        {
            throw new NotImplementedException();
        }

        public void Delete(int iD)
        {
            throw new NotImplementedException();
        }
    }
}