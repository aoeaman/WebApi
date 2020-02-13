using System.Collections.Generic;
using System;
using System.Linq;
using CarPoolApplication.Models;
using CodeFirst.Models;
using CodeFirst.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Http;

namespace CarPoolApplication.Services
{
    public class BookingService : IBookingService
    {
        private readonly IServiceScope _scope;
        public BookingService(IServiceProvider service)
        {
            _scope = service.CreateScope();
        }

        public void UpdateStatus(int iD, StatusOfRide status)
        {
            GetByID(iD).Status = status;
            _scope.ServiceProvider.GetRequiredService<Context>().SaveChanges();
        }

        public HttpResponseException Add(Booking entity)
        {
            try
            {
                var _context = _scope.ServiceProvider.GetRequiredService<Context>();
                entity.Status = StatusOfRide.Pending;
                _context.Bookings.Add(entity);
                _context.SaveChanges();
                return new HttpResponseException(System.Net.HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

        }

        public List<Booking> GetAll()
        {
            return _scope.ServiceProvider.GetRequiredService<Context>().Bookings.ToList();
        }

        public List<Booking> Requests(int id)
        {          
            return _scope.ServiceProvider.GetRequiredService<Context>().Bookings.ToList().FindAll(_ => _.OfferID == id && _.Status == StatusOfRide.Pending);
        }
        public HttpResponseException Delete(int iD)
        {
            try
            {
                var _context = _scope.ServiceProvider.GetRequiredService<Context>();
                _context.Bookings.Remove(_context.Bookings.FirstOrDefault(_ => _.ID == iD));
                _context.SaveChanges();
                return new HttpResponseException(System.Net.HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }
           
        }

        public Booking GetByID(int id)
        {            
            return _scope.ServiceProvider.GetRequiredService<Context>().Bookings.Find(id);
        }

        public IList<Booking> GetByRiderID(int id)
        {
            return _scope.ServiceProvider.GetRequiredService<Context>().Bookings.ToList().FindAll(_ => _.RiderID==id);
        }

        public IList<Booking> GetByOfferID(int id)
        {           
            return _scope.ServiceProvider.GetRequiredService<Context>().Bookings.ToList().FindAll(_ => _.OfferID == id);
        }
    }

}
