using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public interface IOfferService : IService<Offer>
    {

        Offer Update(Offer Offer);
        void Delete(int offerID);
        Offer GetByID(int offerID);

    }
}
