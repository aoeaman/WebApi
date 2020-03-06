using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using CarPool.Data.Models;
using CarPool.Services.Contracts;
using CarPool.Helpers;

namespace CarPool.Services.Providers
{
    public class BookingService : IBookingService
    {
        private readonly IServiceScope _scope;
        readonly Context _context;
        public BookingService(IServiceProvider service)
        {
            _scope = service.CreateScope();
            _context = _scope.ServiceProvider.GetRequiredService<Context>();
        }

        public string UpdateStatus(int id, StatusOfRide status)
        {
            
            try
            {
                GetByID(id).Status = status;
                _context.SaveChanges();
                return Status.Ok.ToString();
            }
            catch (Exception)
            {
                return Status.NotFound.ToString();
            }
        }

        public BookingDBO Add(BookingDBO entity)
        {
            try
            {               
                entity.Status = StatusOfRide.Pending;
                entity.IsActive = true;
                _context.Bookings.Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public List<BookingDBO> GetAll()
        {
            try
            {
                return _context.Bookings.ToList();
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public List<BookingDBO> Requests(int id)
        {          
            return _context.Bookings.ToList().FindAll(b => b.OfferID == id && b.Status == StatusOfRide.Pending);
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

        public BookingDBO GetByID(int id)
        {
            try
            {
                return _context.Bookings.Find(id);
            }
            catch
            {
                return null;
            }
            
        }

        public IList<BookingDBO> GetByRiderID(int id)
        {
            try
            {
                return _context.Bookings.ToList().FindAll(b => b.UserID == id);
            }
            catch
            {
                return null;
            }
            
        }

        public IList<BookingDBO> GetByOfferID(int id)
        {
            try
            {
                return _context.Bookings.ToList().FindAll(b => b.OfferID == id);
            }
            catch
            {
                return null;
            }
            
        }
    }

}
