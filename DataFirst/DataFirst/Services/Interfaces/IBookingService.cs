using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public interface IBookingService:IService<Booking>
    {
        void UpdateStatus(int iD, StatusOfRide status);
        void Delete(int iD);
    }
}
