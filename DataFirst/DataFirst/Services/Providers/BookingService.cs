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
                GetByID(id).Status = status;
                _context.SaveChanges();
                return new HttpResponseException(System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
        }

        public HttpResponseException Add(Booking entity)
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
                entity.Status = StatusOfRide.Pending;
                entity.IsActive = true;
                _context.Bookings.Add(entity);
                _context.SaveChanges();
                return new HttpResponseException(System.Net.HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.Conflict);
            }

        }

        public List<Booking> GetAll()
        {
            try
            {
                return _scope.ServiceProvider.GetRequiredService<Context>().Bookings.ToList();
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public List<Booking> Requests(int id)
        {          
            return _scope.ServiceProvider.GetRequiredService<Context>().Bookings.ToList().FindAll(_ => _.OfferID == id && _.Status == StatusOfRide.Pending);
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
                _context.Bookings.Find(id).IsActive=false;
                _context.SaveChanges();
                return new HttpResponseException(System.Net.HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
           
        }

        public Booking GetByID(int id)
        {
            try
            {
                return _scope.ServiceProvider.GetRequiredService<Context>().Bookings.Find(id);
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
                return _scope.ServiceProvider.GetRequiredService<Context>().Bookings.ToList().FindAll(_ => _.UserID == id);
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
                return _scope.ServiceProvider.GetRequiredService<Context>().Bookings.ToList().FindAll(_ => _.OfferID == id);
            }
            catch
            {
                return null;
            }
            
        }
    }

}
