using System.Web.Http;
using CarPoolApplication.Concerns;

namespace CodeFirst.Services.Interfaces
{
    public interface IVehicleService : IBaseService<Vehicle>
    {
        string Disable(int id);
    }
}
