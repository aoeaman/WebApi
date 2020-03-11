using System.Collections.Generic;
using CarPool.Application.Models;
using CarPool.Data.Models;

namespace CarPool.Services.Contracts
{
    public interface IBookingService : IBaseService<Booking>
    {
        IList<Booking> GetByRiderID(int id);

        string UpdateStatus(int id, StatusOfRide status);       
        IList<Booking> GetByOfferID(int id);
        List<Booking> Requests(int id);
    }
}
