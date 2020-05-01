using CarPool.Application.Models;
using CarPool.Data.Models;
using CarPool.Helpers;
using System;
using System.Collections.Generic;

namespace CarPool.Services.Contracts
{
    public interface IOfferService : IBaseService<Offer>
    {
        Offer Update(Offer offer);
        string UpdateStatus(int id, StatusOfRide status);
        List<Offer> FilterOffer(Cities source, Cities destination,int seats,DateTime dateTime);
        List<Offer> GetByDriver(int id);
        bool ValidateOfferRoute(OfferDBO offerDBO, Cities source, Cities destination, int seats);
    }
}
