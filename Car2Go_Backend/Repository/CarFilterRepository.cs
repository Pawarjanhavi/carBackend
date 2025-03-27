using Car2Go.Data;
using Car2Go.DTOs;
using Car2Go.Models;
using Car2Go.Services;
using System.Drawing;

namespace Car2Go.Repository
{
    public class CarFilterRepository:ICarFilterService
    {
        Car2GoDBContext _db;
        public CarFilterRepository(Car2GoDBContext db) { _db = db; }

        //Filter car details by Make and model
        public List<CarWithLocationDto> GetCarsByModelAndMake(string make, string model)
        {
            var filterResult = _db.Cars.Where(c => 
            c.Make.Replace(" ", "").ToLower().Trim() == make.Replace(" ", "").ToLower().Trim() && 
            c.Model.Replace(" ","").ToLower().Trim() == model.Replace(" ", "").ToLower().Trim()).ToList();


            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        //Filter car details by Make and Colour

        public List<CarWithLocationDto> GetCarsByMakeAndColour( string make,string colour)
        {
            var filterResult = _db.Cars.Where(c => 
            c.Make.Replace(" ", "").ToLower().Trim() == make.Replace(" ", "").ToLower().Trim() && 
            c.Colour.Replace(" ", "").ToLower().Trim() == colour.Replace(" ", "").ToLower().Trim()).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        //Filter car details by Make and Price
        public List<CarWithLocationDto> GetCarsByMakeAndPrice(string make,decimal price)
        {
            var filterResult = _db.Cars.Where(c => 
            c.Make.Replace(" ", "").ToLower().Trim() == make.Replace(" ", "").ToLower().Trim() &&
            c.PricePerDay == price).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();
            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        //Filter car details by Make and Seats
        public List<CarWithLocationDto> GetCarsByMakeAndSeats(string make,int seats)
        {
            var filterResult = _db.Cars.Where(c =>
            c.Make.Replace("  ", "").ToLower().Trim() == make.Replace("  ", "").ToLower().Trim() 
            && c.TotalSeats == seats).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        //Filter car details by Make and Available status
        public List<CarWithLocationDto> GetCarsByMakeAndAvailability(string make, bool availableStatus)
        {
            var filterResult = _db.Cars.Where(c => 
            c.Make.Replace("  ", "").ToLower().Trim() == make.Replace("  ", "").ToLower().Trim() &&
            c.AvailableStatus == availableStatus).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        //Filter car details by Make and available dates
        public List<CarWithLocationDto> GetCarsByMakeAndAvailableDates(string make, DateOnly availableDate)
        {
            var filterResult = _db.Cars.Where(c => 
            c.Make.Replace("  ", "").ToLower().Trim() == make.Replace("  ", "").ToLower().Trim() &&
            c.AvailableDate == availableDate).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        //**************************************************************************************************************

        public List<CarWithLocationDto> GetCarsByModelAndPrice(string model, decimal price)
        {
            var filterResult = _db.Cars.Where(c => 
            c.Model.Replace(" ", "").ToLower().Trim() == model.Replace(" ", "").ToLower().Trim() && 
            c.PricePerDay == price).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        public List<CarWithLocationDto> GetCarsByModelAndSeats(string model, int seats)
        {
            var filterResult = _db.Cars.Where(c => 
            c.Model.Replace(" ", "").ToLower().Trim() == model.Replace(" ", "").ToLower().Trim() &&
            c.TotalSeats == seats).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        public List<CarWithLocationDto> GetCarsByModelAndAvailability(string model, bool availableStatus)
        {
            var filterResult = _db.Cars.Where(c => 
            c.Model.Replace(" ", "").ToLower().Trim() == model.Replace(" ", "").ToLower().Trim() &&
            c.AvailableStatus == availableStatus).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        public List<CarWithLocationDto> GetCarsByModelAndAvailableDates(string model, DateOnly availableDate)
        {
            var filterResult = _db.Cars.Where(c =>
            c.Model.Replace("  ", "").ToLower().Trim() == model.Replace(" ", "").ToLower().Trim() && 
            c.AvailableDate == availableDate).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        //**************************************************************************************************************


        public List<CarWithLocationDto> GetCarsByColourAndPrice(string colour, decimal price)
        {
            var filterResult = _db.Cars.Where(c => 
            c.Colour.Replace(" ", "").ToLower().Trim() == colour.Replace(" ", "").ToLower().Trim() && 
            c.PricePerDay == price).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        public List<CarWithLocationDto> GetCarsByColourAndSeats(string colour, int seats)
        {
            var filterResult = _db.Cars.Where(c => 
            c.Colour.Replace("  ", "").ToLower().Trim() == colour.Replace("  ", "").ToLower().Trim() && 
            c.TotalSeats == seats).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        public List<CarWithLocationDto> GetCarsByColourAndAvailabilty(string colour, bool availableStatus)
        {
            var filterResult = _db.Cars.Where(c =>
            c.Colour.Replace("  ","").ToLower().Trim() == colour.Replace("  ", "").ToLower().Trim() &&
            c.AvailableStatus == availableStatus).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        public List<CarWithLocationDto> GetCarsByColourAndAvailableDates(string colour, DateOnly availableDate)
        {
            var filterResult = _db.Cars.Where(c => 
            c.Colour.Replace(" ","").ToLower().Trim() == colour.Replace(" ", "").ToLower().Trim() &&
            c.AvailableDate == availableDate).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }


        //**************************************************************************************************************

        public List<CarWithLocationDto> GetCarsByPriceAndSeats(decimal price, int seats)
        {
            var filterResult = _db.Cars.Where(c => c.PricePerDay == price && c.TotalSeats == seats).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        public List<CarWithLocationDto> GetCarsByPriceAndAvailability(decimal price, bool availableStatus)
        {

            var filterResult = _db.Cars.Where(c => c.PricePerDay == price && c.AvailableStatus == availableStatus).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        public List<CarWithLocationDto> GetCarsByPriceAndAvailableDates(decimal price, DateOnly availableDate)
        {
            var filterResult = _db.Cars.Where(c => c.PricePerDay == price && c.AvailableDate == availableDate).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        //**************************************************************************************************************


        public List<CarWithLocationDto> GetCarsBySeatsAndAvailability(int seats, bool availableStatus)
        {
            var filterResult = _db.Cars.Where(c => c.TotalSeats == seats && c.AvailableStatus == availableStatus).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        public List<CarWithLocationDto> GetCarsBySeatsAndAvailableDates(int seats, DateOnly availableDate)
        {
            var filterResult = _db.Cars.Where(c => c.TotalSeats == seats && c.AvailableDate == availableDate).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        //**************************************************************************************************************
        List<CarWithLocationDto> ICarFilterService.GetCarsByAvailabilityAndAvailableDates(bool availableStatus, DateOnly availableDate)

        {

            var filterResult = _db.Cars.Where(c => c.AvailableStatus == availableStatus && c.AvailableDate == availableDate).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

        //**************************************************************************************************************

        public List<CarWithLocationDto> GetCarsByAllCarFilters(string make, string model, string colour, decimal price, int seats, bool availableStatus, DateOnly availableDate)
        {
            var filterResult = _db.Cars.Where(c => 
            c.Make.Replace(" ", "").ToLower().Trim() == make.Replace(" ", "").ToLower().Trim() && 
            c.Model.Replace(" ", "").ToLower().Trim() == model.Replace(" ", "").ToLower().Trim() && 
            c.Colour.Replace(" ", "").ToLower().Trim() == colour.Replace(" ", "").ToLower().Trim() && 
            c.PricePerDay == price && c.TotalSeats == seats && 
            c.AvailableStatus == availableStatus && 
            c.AvailableDate == availableDate).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);

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
            return result;
        }

       
    }
}
