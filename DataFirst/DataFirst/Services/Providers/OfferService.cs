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
            try
            {
                var _context = _scope.ServiceProvider.GetRequiredService<Context>();
                offer.CurrentLocaton = offer.Source;
                offer.Status = StatusOfRide.Created;
                _context.Offers.Add(offer);
                _context.SaveChanges();
                return new HttpResponseException(System.Net.HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
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
        public HttpResponseException UpdateStatus(int iD, StatusOfRide status)
        {
            try
            {
                var _context = _scope.ServiceProvider.GetRequiredService<Context>();
                var offer = _context.Offers.Find(iD);
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
                            return new HttpResponseException(System.Net.HttpStatusCode.NotFound);
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
                            return new HttpResponseException(System.Net.HttpStatusCode.OK); ;
                        }
                        else
                        {
                            return new HttpResponseException(System.Net.HttpStatusCode.NotFound);
                        }
                }
                return new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
        }
        public Offer GetByID(int iD)
        {
            try
            {
                return _scope.ServiceProvider.GetRequiredService<Context>().Offers.Find(iD);
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
        public HttpResponseException Delete(int iD)
        {
            try
            {
                var _context = _scope.ServiceProvider.GetRequiredService<Context>();
                _context.Offers.Remove(_context.Offers.Find(iD));
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
                return GetAll().FindAll(_ => _.ID == id);
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

            catch (Exception)
            {
                return null;
            }
        }
    }
}

