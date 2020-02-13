using System.Collections.Generic;
using System;
using System.Linq;
using CarPoolApplication.Models;
using CodeFirst.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using CodeFirst.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolApplication.Services
{
    public class BookingService : IBookingService
    {
        UtilityService Util;
        private readonly IServiceScope _scope;
        public BookingService(IServiceProvider service)
        {
            _scope = service.CreateScope();
            Util = new UtilityService();
        }

        public void UpdateStatus(int iD, StatusOfRide status)
        {
            GetByID(iD).Status = status;
            _scope.ServiceProvider.GetRequiredService<Context>().SaveChanges();
        }

        public void Add(Booking entity)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            _context.Bookings.Add(entity);
            _context.SaveChanges();
        }

        public Booking Create(Booking entity)
        {           
            entity.ID = Util.GenerateID();
            entity.Status = StatusOfRide.Pending;
            return entity;
        }

        public List<Booking> GetAll()
        {
            return _scope.ServiceProvider.GetRequiredService<Context>().Bookings.ToList();
        }

        public List<Booking> Requests(int id)
        {          
            return _scope.ServiceProvider.GetRequiredService<Context>().Bookings.ToList().FindAll(_ => _.OfferID == id && _.Status == StatusOfRide.Pending);
        }
        public void Delete(int iD)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            _context.Bookings.Remove(_context.Bookings.FirstOrDefault(_ => _.ID == iD));
            _context.SaveChanges();
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
