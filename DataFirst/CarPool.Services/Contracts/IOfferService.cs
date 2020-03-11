using CarPool.Application.Models;
using CarPool.Data.Models;
using CarPool.Helpers;
using System.Collections.Generic;

namespace CarPool.Services.Contracts
{
    public interface IOfferService : IBaseService<Offer>
    {
        Offer Update(Offer offer);
        string UpdateStatus(int id, Data.Models.StatusOfRide status);
        List<Offer> FilterOffer(Data.Models.Cities source, Cities destination,int seats);
        List<Offer> GetByDriver(int id);
        bool ValidateOfferRoute(OfferDBO offerDBO, Cities source, Cities destination, int seats);
    }
}
