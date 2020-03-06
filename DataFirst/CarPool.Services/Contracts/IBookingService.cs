using System.Collections.Generic;
using CarPool.Data.Models;

namespace CarPool.Services.Contracts
{
    public interface IBookingService : IBaseService<BookingDBO>
    {
        IList<BookingDBO> GetByRiderID(int id);

        string UpdateStatus(int id, StatusOfRide status);       
        IList<BookingDBO> GetByOfferID(int id);
        List<BookingDBO> Requests(int id);
    }
}
