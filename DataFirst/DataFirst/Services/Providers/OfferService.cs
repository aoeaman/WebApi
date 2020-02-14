using System;
using System.Collections.Generic;
using CarPoolApplication.Models;
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
        public OfferService(IServiceProvider service)
        {
            _scope = service.CreateScope();           
        }
        public HttpResponseException Add(Offer offer)
        {
            Context _context;
            try
            {
                _context = _scope.ServiceProvider.GetRequiredService<Context>();
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.BadGateway);
            }
            try
            {                
                offer.CurrentLocaton = offer.Source;
                offer.Status = StatusOfRide.Created;
                offer.IsActive = true;
                _context.Offers.Add(offer);
                _context.SaveChanges();
                return new HttpResponseException(System.Net.HttpStatusCode.Created);
            }
            catch (Exception)
            {
                _context.Offers.Remove(offer);
                return new HttpResponseException(System.Net.HttpStatusCode.Conflict);
            }            
        }
        public List<Offer> GetAll()
        {
            try
            {
                return _scope.ServiceProvider.GetRequiredService<Context>().Offers.ToList();
            }
            catch (Exception)
            {
                return null;
            }

        }
        public HttpResponseException UpdateStatus(int id, StatusOfRide status)
        {
            Context _context;
            try
            {
                _context = _scope.ServiceProvider.GetRequiredService<Context>();
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.BadGateway);
            }
            try
            {
                var offer = _context.Offers.Find(id);
                switch (status)
                {
                    case StatusOfRide.Cancelled:
                        if (offer.Status == StatusOfRide.Created && offer.Source == offer.CurrentLocaton)
                        {
                            offer.Status = status;
                            return new HttpResponseException(System.Net.HttpStatusCode.OK);
                        }
                        else
                        {
                            return new HttpResponseException(System.Net.HttpStatusCode.NotModified);
                        }

                    case StatusOfRide.Completed:
                        if (offer.Status == StatusOfRide.Created)
                        {
                            offer.Status = status;
                            _context.Bookings.ToList().ForEach(_ =>
                            {
                                if (_.OfferID == id)
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
                            return new HttpResponseException(System.Net.HttpStatusCode.OK); ;
                        }
                        else
                        {
                            return new HttpResponseException(System.Net.HttpStatusCode.NotModified);
                        }
                }
                return new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.MethodNotAllowed);
            }
        }
        public Offer GetByID(int id)
        {
            try
            {
                return _scope.ServiceProvider.GetRequiredService<Context>().Offers.Find(id);
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
        public HttpResponseException Delete(int id)
        {
            Context _context;
            try
            {
                _context = _scope.ServiceProvider.GetRequiredService<Context>();
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.BadGateway);
            }
            try
            {              
                _context.Offers.Find(id).IsActive=false;
                return new HttpResponseException(System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
        }
        public List<Offer> GetByDriver(int id)
        {
            try
            {
                return GetAll().FindAll(_ => _.UserID == id);
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
                var _context = _scope.ServiceProvider.GetRequiredService<Context>();
                List<Offer> Offers = new List<Offer>();
                foreach (Offer offer in GetAll().FindAll(_ => _.Status == StatusOfRide.Created))
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
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            
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
                List<Booking> AssociatedBookings = _context.Bookings.ToList().FindAll(_ => _.OfferID == offer.ID && _.Status == StatusOfRide.Accepted);
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

