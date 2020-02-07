using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services.Interfaces
{
    public interface IVehicleService : IService<Vehicle>
    {
        Vehicle GetVehicleByID(int iD);

    }
}
