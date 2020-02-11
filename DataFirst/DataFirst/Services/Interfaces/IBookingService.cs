using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CodeFirst.Services.Interfaces
{
    public interface IBookingService : IService<Booking>
    {
        IList<Booking> GetByRiderID(int id);

        void UpdateStatus(int iD, StatusOfRide status);       
        void Cancel(int id);
        IList<Booking> GetByOfferID(int id);
    }
}
