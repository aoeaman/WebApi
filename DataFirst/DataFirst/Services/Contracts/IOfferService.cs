using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CodeFirst.Services.Interfaces
{
    public interface IOfferService : IBaseService<Offer>
    {
        Offer Update(Offer Offer);
        void Cancel(int id);
        List<Offer> Requests(int id);
        List<Offer> FilterOffer(Cities source, Cities destination,int seats);
    }
}
