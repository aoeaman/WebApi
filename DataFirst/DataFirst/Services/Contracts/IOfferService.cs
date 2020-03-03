﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using CarPoolApplication.Concerns;

namespace CodeFirst.Services.Interfaces
{
    public interface IOfferService : IBaseService<Offer>
    {
        Offer Update(Offer offer);
        string UpdateStatus(int id, StatusOfRide status);
        List<Offer> FilterOffer(Cities source, Cities destination,int seats);
    }
}
