using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CodeFirst.Services.Interfaces
{
    public interface IOfferService : IService<Offer>
    {

        Offer Update(Offer Offer);

    }
}
