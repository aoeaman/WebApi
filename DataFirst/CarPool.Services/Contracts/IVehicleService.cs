using CarPool.Application.Models;

namespace CarPool.Services.Contracts
{
    public interface IVehicleService : IBaseService<Vehicle>
    {
        string Disable(int id);
    }
}
