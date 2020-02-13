using System;
using System.Collections.Generic;
using CarPoolApplication.Models;
using CodeFirst.Services.Interfaces;
using CodeFirst.Models;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace CarPoolApplication.Services
{
    public class OfferService:IOfferService

    {
        readonly UtilityService Util;
        private readonly IServiceScope _scope;
        public OfferService(IServiceProvider service)
        {
            _scope = service.CreateScope();
            Util = new UtilityService();            
        }
        public void Add(Offer offer)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            _context.Offers.Add(offer);
            _context.SaveChanges();
        }       
        public Offer Create(Offer offer)
        {
            offer.ID = Util.GenerateID();
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
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            return _context.Offers.ToList() ;
        }
        public bool UpdateStatus(int iD, StatusOfRide status)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            var offer = _context.Offers.Find(iD);
            switch (status)
            {
                case StatusOfRide.Cancelled:                    
                    if (offer.Status == StatusOfRide.Created && offer.Source == offer.CurrentLocaton) { offer.Status = status; return true; }
                    else
                    {
                        return false;
                    }

                case StatusOfRide.Completed:
                    if (offer.Status == StatusOfRide.Created)
                    {
                        offer.Status = status;
                        _context.Bookings.ToList().ForEach(_ =>
                        {
                            if (_.OfferID == iD)
                            {
                                if (_.Status == StatusOfRide.Accepted)
                                {
                                    _.Status = StatusOfRide.Completed;
                                }
                                else
                                {
                                    _.Status = StatusOfRide.Rejected;
                                }
                            }
                        });
                        return true;
                    }
                    else return false;
            }
            return false;
        }
        public Offer GetByID(int iD)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            return _context.Offers.Find(iD);
        }
        public Offer Update(Offer Offer)
        {
            throw new NotImplementedException();
        }
        public void Delete(int iD)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            _context.Offers.Remove(_context.Offers.Find(iD));
        }
        public List<Offer> GetByDriver(int id)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            return GetAll().FindAll(_ => _.ID == id);
        }
        public List<Offer> FilterOffer(Cities source, Cities destination,int seats)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();      
            List<Offer> Offers = new List<Offer>();
            foreach (Offer offer in GetAll().FindAll(_ => _.Status == StatusOfRide.Created))
            {
                int MaxSeats = offer.SeatsAvailable;
                List<Cities> OfferSequence = _context.ViaPoints.Where(P => P.OfferID == offer.ID).Select(p => p.City).ToList();
                
                OfferSequence.Insert(0, offer.Source);
                OfferSequence.Insert(OfferSequence.Count, offer.Destination);
                if (OfferSequence.IndexOf(offer.CurrentLocaton) > OfferSequence.IndexOf(offer.Source))
                {
                    OfferSequence.RemoveRange(OfferSequence.IndexOf(offer.Source), OfferSequence.IndexOf(offer.CurrentLocaton));
                }

                if (OfferSequence.IndexOf(source) != -1 && OfferSequence.IndexOf(source) < OfferSequence.IndexOf(destination))
                {
                    List<Booking> AssociatedBookings = _context.Bookings.ToList().FindAll(_ => _.OfferID == offer.ID && _.Status == StatusOfRide.Accepted);
                    bool Flag = false;
                    foreach (Cities Node in OfferSequence)
                    {
                        if (Node == destination)
                        {
                            if (MaxSeats >= seats)
                            {
                                Offers.Add(offer);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        foreach (Booking Element in AssociatedBookings)
                        {
                            if (Node == Element.Source)
                            {
                                MaxSeats -= Element.Seats;
                            }
                            else if (Node == Element.Destination)
                            {
                                MaxSeats += Element.Seats;
                            }

                        }
                        if (Node == source)
                        {
                            if (seats > MaxSeats)
                            {
                                break;
                            }
                            else
                            {
                                Flag = true;
                            }
                        }
                        if (Flag && seats > MaxSeats)
                        {
                            break;
                        }
                    }
                }
            }
            return Offers;
        }
    }
}