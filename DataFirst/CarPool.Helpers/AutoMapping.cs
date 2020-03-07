using AutoMapper;
using CarPool.Data.Models;
using CarPool.Application.Models;

namespace CarPool.Helpers
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDBO>().ReverseMap();
            CreateMap<Booking, BookingDBO>().ReverseMap();
            CreateMap<Vehicle, VehicleDBO>().ReverseMap();
            CreateMap<OfferDBO, Offer>().ReverseMap();
        }
    }
}
