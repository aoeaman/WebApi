
using AutoMapper;
using CarPool.Application.Models;
using CarPool.Data.Models;

namespace CarPool.Automapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDBO>().ReverseMap();
            CreateMap<Booking, BookingDBO>().ReverseMap();
            CreateMap<Vehicle, VehicleDBO>().ReverseMap();
            CreateMap<Offer, OfferDBO>().ReverseMap();
            CreateMap<ViaPoints, ViaPointsDBO>().ReverseMap();
        }
    }
}
