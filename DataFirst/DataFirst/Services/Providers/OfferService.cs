using System;
using System.Collections.Generic;
using CarPoolApplication.Concerns;
using CodeFirst.Services.Interfaces;
using CodeFirst.Models;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Http;

namespace CarPoolApplication.Services
{
    public class OfferService:IOfferService

    {
        private readonly IServiceScope _scope;
        readonly Context _context;
        public OfferService(IServiceProvider service)
        {
            _scope = service.CreateScope();
            _context = _scope.ServiceProvider.GetRequiredService<Context>();
        }
        public Offer Add(Offer offer)
        {

            try
            {                
                offer.CurrentLocaton = offer.Source;
                offer.Status = StatusOfRide.Created;
                offer.IsActive = true;
                _context.Offers.Add(offer);
                _context.SaveChanges();
                return offer;
            }
            catch (Exception)
            {
                _context.Offers.Remove(offer);
                return null;
            }            
        }
        public List<Offer> GetAll()
        {
            try
            {
                return _context.Offers.ToList();
            }
            catch (Exception)
            {
                return null;
            }

        }
        public string UpdateStatus(int id, StatusOfRide status)
        {
            try
            {
                var offer = _context.Offers.Find(id);
                switch (status)
                {
                    case StatusOfRide.Cancelled:
                        if (offer.Status == StatusOfRide.Created && offer.Source == offer.CurrentLocaton)
                        {
                            offer.Status = status;
                            return Status.Ok.ToString();
                        }
                        else
                        {
                            return Status.UnableToPerformAction.ToString();
                        }

                    case StatusOfRide.Completed:
                        if (offer.Status == StatusOfRide.Created)
                        {
                            offer.Status = status;
                            _context.Bookings.ToList().ForEach(b =>
                            {
                                if (b.OfferID == id)
                                {
                                    if (b.Status == StatusOfRide.Accepted)
                                    {
                                        b.Status = StatusOfRide.Completed;
                                    }
                                    else
                                    {
                                        b.Status = StatusOfRide.Rejected;
                                    }
                                }
                            });
                            return Status.Ok.ToString();
                        }
                        else
                        {
                            return Status.UnableToPerformAction.ToString(); 
                        }
                }
                return Status.NotFound.ToString();
            }
            catch (Exception)
            {
                return Status.Failed.ToString();
            }
        }
        public Offer GetByID(int id)
        {
            try
            {
                return _context.Offers.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Offer Update(Offer Offer)
        {
            throw new NotImplementedException();
        }
        public string Delete(int id)
        {
            try
            {              
                _context.Offers.Find(id).IsActive=false;
                return Status.Ok.ToString();
            }
            catch (Exception)
            {
                return Status.NotFound.ToString();
            }
        }
        public List<Offer> GetByDriver(int id)
        {
            try
            {
                return GetAll().FindAll(p => p.UserID == id);
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public List<Offer> FilterOffer(Cities source, Cities destination, int seats)
        {
            try
            {
                List<Offer> Offers = new List<Offer>();
                foreach (Offer offer in GetAll().FindAll(o => o.Status == StatusOfRide.Created && o.IsActive==true))
                {
                    if (ValidateOfferRoute(offer, source, destination, seats))
                    {
                        Offers.Add(offer);
                    }
                    else
                    {
                        continue;
                    }
                }
                return Offers;
            }

            catch (Exception)
            {
                return null;
            }
        }
        bool ValidateOfferRoute(Offer offer,Cities source,Cities destination,int seats)
        {        
            int MaxSeats = offer.SeatsAvailable;
            List<Cities> Route = _context.ViaPoints.Where(P => P.OfferID == offer.ID).Select(p => p.City).ToList();

            Route.Insert(0, offer.Source);
            Route.Insert(Route.Count, offer.Destination);

            if (Route.IndexOf(offer.CurrentLocaton) > Route.IndexOf(offer.Source))
            {
                Route.RemoveRange(Route.IndexOf(offer.Source), Route.IndexOf(offer.CurrentLocaton));
            }

            if (Route.IndexOf(source) != -1 && Route.IndexOf(source) < Route.IndexOf(destination))
            {
                List<Booking> AssociatedBookings = _context.Bookings.ToList().FindAll(b => b.OfferID == offer.ID && b.Status == StatusOfRide.Accepted);
                bool Flag = false;
                foreach (Cities Node in Route)
                {
                    if (Node == destination)
                    {
                        if (MaxSeats >= seats)
                        {
                            return true;
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
            return false;
        }
    }
}

