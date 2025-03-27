using Car2Go.Data;
using Car2Go.DTOs;
using Car2Go.Models;
using Car2Go.Services;
using Microsoft.AspNetCore.Mvc;

namespace Car2Go.Repository
{
    public class CarFilterByLocationRepository:ICarFilterByLocationService
    {
        Car2GoDBContext _db;
        public CarFilterByLocationRepository(Car2GoDBContext db)
        {
            _db = db;
        }

        //Search car details by City 
        public List<CarWithLocationDto> GetCarByCity(string city)
        {
            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            var getLocation = _db.Locations.Where(l => l.City.Replace(" ", "").ToLower().Trim() == city.Replace(" ", "").ToLower().Trim()).ToList();

            if (getLocation.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var location in getLocation)
            {
                var locationOfFilterResult = _db.Locations.Find(location.LocationId);

                var findCar = _db.Cars.Where(c => c.LocationId == locationOfFilterResult.LocationId).ToList();

                if (findCar.Count < 0)
                {
                    return result = new() { };
                }

                foreach (var car in findCar) {

                    result.Add(new CarWithLocationDto()
                    {
                        Make = car.Make,
                        Model = car.Model,
                        year = car.year,
                        Colour = car.Colour,
                        TotalSeats = car.TotalSeats,
                        LicensePlate = car.LicensePlate,
                        PricePerDay = car.PricePerDay,
                        //Description = car.Description,
                        AvailableStatus = car.AvailableStatus,
                        AvailableDate = car.AvailableDate,
                        City = locationOfFilterResult.City,
                        Address = locationOfFilterResult.Address,
                        State = locationOfFilterResult.State,
                        Country = locationOfFilterResult.Country,
                        ZipCode = locationOfFilterResult.ZipCode,

                    });
                }
            
            }
            return result;

        }

        //Search car details by State 

        public List<CarWithLocationDto> GetCarByState(string state)
        {
            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            var getLocation = _db.Locations.Where(l => l.State.Replace(" ", "").ToLower().Trim() == state.Replace(" ", "").ToLower().Trim()).ToList();

            if (getLocation.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var location in getLocation)
            {
                var locationOfFilterResult = _db.Locations.Find(location.LocationId);

                var findCar = _db.Cars.Where(c => c.LocationId == locationOfFilterResult.LocationId).ToList();

                if (findCar.Count < 0)
                {
                    return result = new() { };
                }

                foreach (var car in findCar)
                {

                    result.Add(new CarWithLocationDto()
                    {
                        Make = car.Make,
                        Model = car.Model,
                        year = car.year,
                        Colour = car.Colour,
                        TotalSeats = car.TotalSeats,
                        LicensePlate = car.LicensePlate,
                        PricePerDay = car.PricePerDay,
                        //Description = car.Description,
                        AvailableStatus = car.AvailableStatus,
                        AvailableDate = car.AvailableDate,
                        City = locationOfFilterResult.City,
                        Address = locationOfFilterResult.Address,
                        State = locationOfFilterResult.State,
                        Country = locationOfFilterResult.Country,
                        ZipCode = locationOfFilterResult.ZipCode,

                    });
                }

            }
            return result;

        }

        //Search car details by City and State

        public List<CarWithLocationDto> GetCarByCityAndState(string city , string state)
        {
            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            var getLocation = _db.Locations.Where(l => 
            l.City.Replace(" ", "").ToLower().Trim() == city.Replace(" ", "").ToLower().Trim() &&
            l.State.Replace(" ", "").ToLower().Trim() == state.Replace(" ", "").ToLower().Trim()).ToList();

            if (getLocation.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var location in getLocation)
            {
                var locationOfFilterResult = _db.Locations.Find(location.LocationId);

                var findCar = _db.Cars.Where(c => c.LocationId == locationOfFilterResult.LocationId).ToList();

                if (findCar.Count < 0)
                {
                    return result = new() { };
                }

                foreach (var car in findCar)
                {

                    result.Add(new CarWithLocationDto()
                    {
                        Make = car.Make,
                        Model = car.Model,
                        year = car.year,
                        Colour = car.Colour,
                        TotalSeats = car.TotalSeats,
                        LicensePlate = car.LicensePlate,
                        PricePerDay = car.PricePerDay,
                        //Description = car.Description,
                        AvailableStatus = car.AvailableStatus,
                        AvailableDate = car.AvailableDate,
                        City = locationOfFilterResult.City,
                        Address = locationOfFilterResult.Address,
                        State = locationOfFilterResult.State,
                        Country = locationOfFilterResult.Country,
                        ZipCode = locationOfFilterResult.ZipCode,

                    });
                }

            }
            return result;

        }


    }
}
