using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CodeFirst.Services.Interfaces
{
    public interface IBookingService : IBaseService<Booking>
    {
        IList<Booking> GetByRiderID(int id);

        void UpdateStatus(int iD, StatusOfRide status);       
        IList<Booking> GetByOfferID(int id);
    }
}
