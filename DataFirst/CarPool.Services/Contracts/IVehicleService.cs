using CarPool.Data.Models;

namespace CarPool.Services.Contracts
{
    public interface IVehicleService : IBaseService<VehicleDBO>
    {
        string Disable(int id);
    }
}
