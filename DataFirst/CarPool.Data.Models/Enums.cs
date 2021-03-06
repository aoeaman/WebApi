﻿namespace CarPool.Data.Models
{
    public enum StatusOfRide
    {
        Pending,
        Accepted,
        Rejected,
        Cancelled,
        Completed,
        Created,
    }


    public enum Cities
    {
        Ahemadabad,Banglore,Chandigarh,Chennai, Dehradun, Gwalior,Hyderabad,Mumbai,Patna,Pune ,Vizag 
    }

    public enum VehicleType
    {
        Bike,
        Car,
        Jeep
    }

    public enum Status
    {
        DbError,
        Ok,
        NotFound,
        Failed,
        UnableToPerformAction
    }
}
