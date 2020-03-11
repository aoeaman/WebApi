using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using CarPool.Data.Models;
using CarPool.Services.Contracts;
using CodeFirst;
using CarPool.Application.Models;
using AutoMapper;

namespace CarPool.Services.Providers
{
    public class BookingService : IBookingService
    {
        readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IOfferService _offerservice;

        public BookingService(Context context, IMapper mapper,IOfferService offerService)
        {
            _context = context;
            _mapper = mapper;
            _offerservice = offerService;
        }


        public Booking Add(Booking entity)
        {
            var _entity = _mapper.Map<BookingDBO>(entity);
            try
            {               
                _entity.Status = StatusOfRide.Pending;
                _entity.IsActive = true;
                _context.Bookings.Add(_entity);
                _context.SaveChanges();
                return _mapper.Map<Booking>(_entity);
            }
            catch (Exception)
            {
                _context.Bookings.Remove(_entity);
                return null;
            }

        }

        public List<Booking> GetAll()
        {
            try
            {
                List<Booking> Bookings = new List<Booking>();
                foreach (var booking in _context.Bookings)
                {
                    Bookings.Add(_mapper.Map<Booking>(booking));
                }
                return Bookings.ToList();
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public List<Booking> Requests(int id)
        {
            List<Booking> Bookings = new List<Booking>();
            foreach (var booking in _context.Bookings.ToList().FindAll(b => b.OfferID == id && b.Status == StatusOfRide.Pending))
            {
                Bookings.Add(_mapper.Map<Booking>(booking));
            }
            return Bookings;
        }
        public string Delete(int id)
        {

            try
            {
                _context.Bookings.Find(id).IsActive=false;
                _context.SaveChanges();
                return Status.Ok.ToString();
            }
            catch (Exception)
            {
                return Status.NotFound.ToString();
            }
           
        }

        public Booking GetByID(int id)
        {
            try
            {
                return _mapper.Map<Booking>(_context.Bookings.Find(id));
            }
            catch
            {
                return null;
            }
            
        }

        public IList<Booking> GetByRiderID(int id)
        {
            try
            {
                List<Booking> Bookings = new List<Booking>();
                foreach (var booking in _context.Bookings.ToList().FindAll(b => b.UserID == id))
                {
                    Bookings.Add(_mapper.Map<Booking>(booking));
                }
                return Bookings;
            }
            catch
            {
                return null;
            }
            
        }

        public IList<Booking> GetByOfferID(int id)
        {
            try
            {
                List<Booking> Bookings = new List<Booking>();
                foreach (var booking in _context.Bookings.ToList().FindAll(b => b.OfferID == id))
                {
                    Bookings.Add(_mapper.Map<Booking>(booking));
                }
                return Bookings;
            }
            catch
            {
                return null;
            }
            
        }

        public string UpdateStatus(int id, StatusOfRide status)
        {
            try
            {
                var booking = _context.Bookings.Find(Convert.ToInt32(id));
                if (_offerservice.ValidateOfferRoute(_context.Offers.Find(booking.OfferID), booking.Source, booking.Destination, booking.Seats))
                {
                    booking.Status = status;
                    _context.SaveChanges();
                    return Status.Ok.ToString();
                }
                else
                {
                    return Status.UnableToPerformAction.ToString();
                }

            }
            catch (Exception)
            {
                return Status.NotFound.ToString();
            }
        }
    }

}
