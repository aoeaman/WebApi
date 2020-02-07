using CarPoolApplication.Models;
using CarPoolApplication.Services;
using CarPoolApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Controllers
{
    public class BookingController
    {
        private IBookingService _repos;
        public BookingController(IBookingService repos)
        {
            _repos = repos;
        }

        

    }
}
