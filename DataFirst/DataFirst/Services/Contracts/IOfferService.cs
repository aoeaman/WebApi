using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CodeFirst.Services.Interfaces
{
    public interface IOfferService : IBaseService<Offer>
    {
        Offer Update(Offer offer);
        bool UpdateStatus(int id, StatusOfRide status);
        List<Offer> FilterOffer(Cities source, Cities destination,int seats);
    }
}
