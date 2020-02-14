using System.Web.Http;
using CarPoolApplication.Models;

namespace CodeFirst.Services.Interfaces
{
    public interface IVehicleService : IBaseService<Vehicle>
    {
        HttpResponseException Disable(int id);
    }
}
