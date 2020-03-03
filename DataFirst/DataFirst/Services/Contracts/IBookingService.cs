using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using CarPoolApplication.Concerns;

namespace CodeFirst.Services.Interfaces
{
    public interface IBookingService : IBaseService<Booking>
    {
        IList<Booking> GetByRiderID(int id);

        string UpdateStatus(int id, StatusOfRide status);       
        IList<Booking> GetByOfferID(int id);
        List<Booking> Requests(int id);
    }
}
