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
        Context _context;

        public BookingService(IServiceProvider service)
        {
            _scope = service.CreateScope();
            Util = new UtilityService();
            _context = _scope.ServiceProvider.GetRequiredService<Context>();
        }

        public void UpdateStatus(int iD, StatusOfRide status)
        {
            _context.Bookings.Find(iD).Status = status;
            _context.SaveChanges();
        }

        public void Add(Booking entity)
        {
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
            return _context.Bookings.ToList();
        }
        public void Delete(int iD)
        {
            _context.Bookings.Remove(_context.Bookings.FirstOrDefault(_ => _.ID == iD));
            _context.SaveChanges();
        }

        public Booking GetByID(int id)
        {
            return _context.Bookings.Find(id);
        }

        public IList<Booking> GetByRiderID(int id)
        {
            return _context.Bookings.ToList().FindAll(_ => _.RiderID==id);
        }

        public void Cancel(int id)
        {
            _context.Bookings.Find(id).Status = StatusOfRide.Cancelled;
        }

        public IList<Booking> GetByOfferID(int id)
        {
            return _context.Bookings.ToList().FindAll(_ => _.OfferID == id);
        }
    }

}
