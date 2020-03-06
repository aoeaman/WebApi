using CarPool.Data.Models;
using System.Collections.Generic;

namespace CarPool.Services.Contracts
{
    public interface IOfferService : IBaseService<OfferDBO>
    {
        OfferDBO Update(OfferDBO offer);
        string UpdateStatus(int id, StatusOfRide status);
        List<OfferDBO> FilterOffer(Cities source, Cities destination,int seats);
    }
}
