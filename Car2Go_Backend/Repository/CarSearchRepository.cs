using Car2Go.Data;
using Car2Go.DTOs;
using Car2Go.Models;
using Car2Go.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Car2Go.Repository
{
    public class CarSearchRepository:ICarSearchService
    {
        Car2GoDBContext _db;
        public CarSearchRepository(Car2GoDBContext db) { _db = db; }

        //Search car details by Model or Make
        public List<CarWithRatingsDto> GetAllCarsBySearchValue(string searchValue)
        {
            var makePresent = _db.Cars.Where(c => c.Make.Replace(" ", "").ToLower().Trim() == searchValue.Replace(" ", "").ToLower().Trim()).ToList();
            var modelPresent = _db.Cars.Where(c => c.Model.Replace(" ", "").ToLower().Trim() == searchValue.Replace(" ", "").ToLower().Trim()).ToList();


            List<CarWithRatingsDto> result = new List<CarWithRatingsDto>();

            var getLocation = _db.Locations.Where(l => l.State.Replace(" ", "").ToLower().Trim() == searchValue.Replace(" ", "").ToLower().Trim()).ToList();

            var getLocationCity = _db.Locations.Where(l => l.City.Replace(" ", "").ToLower().Trim() == searchValue.Replace(" ", "").ToLower().Trim()).ToList();

            if (getLocationCity.Count > 0)
            {

                foreach (var location in getLocationCity)
                {
                    var locationOfFilterResult = _db.Locations.Find(location.LocationId);

                    var findCar = _db.Cars.Where(c => c.LocationId == locationOfFilterResult.LocationId).ToList();

                    if (findCar.Count < 0)
                    {
                        return result = new() { };
                    }

                    foreach (var car in findCar)
                    {
                        var ratings = _db.Reviews.Where(r => r.CarId == car.CarId);

                        if (ratings.Count() == 0)
                        {

                            result.Add(new CarWithRatingsDto()
                            {
                                Make = car.Make,
                                Model = car.Model,
                                year = car.year,
                                Colour = car.Colour,
                                TotalSeats = car.TotalSeats,
                                LicensePlate = car.LicensePlate,
                                PricePerDay = car.PricePerDay,
                                imageUrl = car.ImageUrl,
                                //Description = car.Description,
                                AvailableStatus = car.AvailableStatus,
                                AvailableDate = car.AvailableDate,
                                City = locationOfFilterResult.City,
                                Address = locationOfFilterResult.Address,
                                State = locationOfFilterResult.State,
                                Country = locationOfFilterResult.Country,
                                ZipCode = locationOfFilterResult.ZipCode,
                                Rating = 0,
                                TotalRatings = 0

                                //LocationId = car.LocationId
                            });
                        }
                        else
                        {
                            var avgRating = 0;
                            var sumOfAllRatings = 0;
                            var ratingCount = 0;

                            foreach (var item in ratings)
                            {
                                sumOfAllRatings += item.Rating;
                                ratingCount++;
                            }

                            avgRating = sumOfAllRatings / ratings.Count();

                            result.Add(new CarWithRatingsDto()
                            {
                                Make = car.Make,
                                Model = car.Model,
                                year = car.year,
                                Colour = car.Colour,
                                TotalSeats = car.TotalSeats,
                                LicensePlate = car.LicensePlate,
                                PricePerDay = car.PricePerDay,
                                imageUrl = car.ImageUrl,
                                //Description = car.Description,
                                AvailableStatus = car.AvailableStatus,
                                AvailableDate = car.AvailableDate,
                                City = locationOfFilterResult.City,
                                Address = locationOfFilterResult.Address,
                                State = locationOfFilterResult.State,
                                Country = locationOfFilterResult.Country,
                                ZipCode = locationOfFilterResult.ZipCode,
                                Rating = avgRating,
                                TotalRatings = ratingCount,

                                //LocationId = car.LocationId
                            });
                        }

                        
                    }
                }
            }

            else if (getLocation.Count > 0)
            {
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
                        var ratings = _db.Reviews.Where(r => r.CarId == car.CarId);
                        if (ratings.Count() == 0)
                        {

                            result.Add(new CarWithRatingsDto()
                            {
                                Make = car.Make,
                                Model = car.Model,
                                year = car.year,
                                Colour = car.Colour,
                                TotalSeats = car.TotalSeats,
                                LicensePlate = car.LicensePlate,
                                PricePerDay = car.PricePerDay,
                                imageUrl = car.ImageUrl,
                                //Description = car.Description,
                                AvailableStatus = car.AvailableStatus,
                                AvailableDate = car.AvailableDate,
                                City = locationOfFilterResult.City,
                                Address = locationOfFilterResult.Address,
                                State = locationOfFilterResult.State,
                                Country = locationOfFilterResult.Country,
                                ZipCode = locationOfFilterResult.ZipCode,
                                Rating = 0,
                                TotalRatings = 0

                                //LocationId = car.LocationId
                            });
                        }
                        else
                        {
                            var avgRating = 0;
                            var sumOfAllRatings = 0;
                            var ratingCount = 0;

                            foreach (var item in ratings)
                            {
                                sumOfAllRatings += item.Rating;
                                ratingCount++;
                            }

                            avgRating = sumOfAllRatings / ratings.Count();

                            result.Add(new CarWithRatingsDto()
                            {
                                Make = car.Make,
                                Model = car.Model,
                                year = car.year,
                                Colour = car.Colour,
                                TotalSeats = car.TotalSeats,
                                LicensePlate = car.LicensePlate,
                                PricePerDay = car.PricePerDay,
                                imageUrl = car.ImageUrl,
                                //Description = car.Description,
                                AvailableStatus = car.AvailableStatus,
                                AvailableDate = car.AvailableDate,
                                City = locationOfFilterResult.City,
                                Address = locationOfFilterResult.Address,
                                State = locationOfFilterResult.State,
                                Country = locationOfFilterResult.Country,
                                ZipCode = locationOfFilterResult.ZipCode,
                                Rating = avgRating,
                                TotalRatings = ratingCount,

                                //LocationId = car.LocationId
                            });
                        }
                    }
                }
            }


            else if (makePresent.Count > 0)
            {
                foreach (var car in makePresent)
                {
                    var locationOfFilterResult = _db.Locations.Find(car.LocationId);
                    var ratings = _db.Reviews.Where(r => r.CarId == car.CarId);

                    if (ratings.Count() == 0)
                    {

                        result.Add(new CarWithRatingsDto()
                        {
                            Make = car.Make,
                            Model = car.Model,
                            year = car.year,
                            Colour = car.Colour,
                            TotalSeats = car.TotalSeats,
                            LicensePlate = car.LicensePlate,
                            PricePerDay = car.PricePerDay,
                            imageUrl = car.ImageUrl,
                            //Description = car.Description,
                            AvailableStatus = car.AvailableStatus,
                            AvailableDate = car.AvailableDate,
                            City = locationOfFilterResult.City,
                            Address = locationOfFilterResult.Address,
                            State = locationOfFilterResult.State,
                            Country = locationOfFilterResult.Country,
                            ZipCode = locationOfFilterResult.ZipCode,
                            Rating = 0,
                            TotalRatings = 0

                            //LocationId = car.LocationId
                        });
                    }
                    else
                    {
                        var avgRating = 0;
                        var sumOfAllRatings = 0;
                        var ratingCount = 0;

                        foreach (var item in ratings)
                        {
                            sumOfAllRatings += item.Rating;
                            ratingCount++;
                        }

                        avgRating = sumOfAllRatings / ratings.Count();

                        result.Add(new CarWithRatingsDto()
                        {
                            Make = car.Make,
                            Model = car.Model,
                            year = car.year,
                            Colour = car.Colour,
                            TotalSeats = car.TotalSeats,
                            LicensePlate = car.LicensePlate,
                            PricePerDay = car.PricePerDay,
                            imageUrl = car.ImageUrl,
                            //Description = car.Description,
                            AvailableStatus = car.AvailableStatus,
                            AvailableDate = car.AvailableDate,
                            City = locationOfFilterResult.City,
                            Address = locationOfFilterResult.Address,
                            State = locationOfFilterResult.State,
                            Country = locationOfFilterResult.Country,
                            ZipCode = locationOfFilterResult.ZipCode,
                            Rating = avgRating,
                            TotalRatings = ratingCount,

                            //LocationId = car.LocationId
                        });
                    }
                }
            }
            else if (modelPresent.Count > 0)
            {
                foreach (var car in modelPresent)
                {
                    var locationOfFilterResult = _db.Locations.Find(car.LocationId);
                    var ratings = _db.Reviews.Where(r => r.CarId == car.CarId);

                    if (ratings.Count() == 0)
                    {

                        result.Add(new CarWithRatingsDto()
                        {
                            Make = car.Make,
                            Model = car.Model,
                            year = car.year,
                            Colour = car.Colour,
                            TotalSeats = car.TotalSeats,
                            LicensePlate = car.LicensePlate,
                            PricePerDay = car.PricePerDay,
                            imageUrl = car.ImageUrl,
                            //Description = car.Description,
                            AvailableStatus = car.AvailableStatus,
                            AvailableDate = car.AvailableDate,
                            City = locationOfFilterResult.City,
                            Address = locationOfFilterResult.Address,
                            State = locationOfFilterResult.State,
                            Country = locationOfFilterResult.Country,
                            ZipCode = locationOfFilterResult.ZipCode,
                            Rating = 0,
                            TotalRatings = 0

                            //LocationId = car.LocationId
                        });
                    }
                    else
                    {
                        var avgRating = 0;
                        var sumOfAllRatings = 0;
                        var ratingCount = 0;

                        foreach (var item in ratings)
                        {
                            sumOfAllRatings += item.Rating;
                            ratingCount++;
                        }

                        avgRating = sumOfAllRatings / ratings.Count();

                        result.Add(new CarWithRatingsDto()
                        {
                            Make = car.Make,
                            Model = car.Model,
                            year = car.year,
                            Colour = car.Colour,
                            TotalSeats = car.TotalSeats,
                            LicensePlate = car.LicensePlate,
                            PricePerDay = car.PricePerDay,
                            imageUrl = car.ImageUrl,
                            //Description = car.Description,
                            AvailableStatus = car.AvailableStatus,
                            AvailableDate = car.AvailableDate,
                            City = locationOfFilterResult.City,
                            Address = locationOfFilterResult.Address,
                            State = locationOfFilterResult.State,
                            Country = locationOfFilterResult.Country,
                            ZipCode = locationOfFilterResult.ZipCode,
                            Rating = avgRating,
                            TotalRatings = ratingCount,

                            //LocationId = car.LocationId
                        });
                    }
                }
            }
            else
            {
                return result = new() { };
            }
           
            return result;

        }

        //Filter car
        //public List<CarWithImageDto> GetCarsByCarFilters(string City, string State, string Colour, int year, int TotalSeats, bool AvailableStatus, decimal minPrice, decimal maxPrice)
        //{
        //    var location = _db.Locations.FirstOrDefault(l =>
        //    l.City.Replace(" ", "").ToLower().Trim() == City.Replace(" ", "").ToLower().Trim() &&
        //     l.State.Replace(" ", "").ToLower().Trim() == State.Replace(" ", "").ToLower().Trim());

        //    List<CarWithImageDto> result = new List<CarWithImageDto>();

        //    if (location == null)
        //    {
        //        return result = new() { };
        //    }

        //    var filterResult = _db.Cars.Where(c =>
        //    c.Colour.Replace(" ", "").ToLower().Trim() == Colour.Replace(" ", "").ToLower().Trim() &&
        //    (c.PricePerDay >= minPrice && c.PricePerDay <= maxPrice) && c.TotalSeats == TotalSeats &&
        //    c.AvailableStatus == AvailableStatus && c.LocationId == location.LocationId).ToList();


        //    if (filterResult.Count <= 0)
        //    {
        //        return result = new() { };
        //    }

        //    foreach (var car in filterResult)
        //    {
        //        var locationOfFilterResult = _db.Locations.Find(car.LocationId);

        //        result.Add(new CarWithImageDto()
        //        {
        //            Make = car.Make,
        //            Model = car.Model,
        //            year = car.year,
        //            Colour = car.Colour,
        //            TotalSeats = car.TotalSeats,
        //            LicensePlate = car.LicensePlate,
        //            PricePerDay = car.PricePerDay,
        //            imageUrl = car.ImageUrl,
        //            //Description = car.Description,
        //            AvailableStatus = car.AvailableStatus,
        //            AvailableDate = car.AvailableDate,
        //            City = locationOfFilterResult.City,
        //            Address = locationOfFilterResult.Address,
        //            State = locationOfFilterResult.State,
        //            Country = locationOfFilterResult.Country,
        //            ZipCode = locationOfFilterResult.ZipCode,

        //        });
        //    }
        //    return result;
        //}

        //Search car details by Model name
        public List<CarWithLocationDto> GetCarByModel(string model)
        {
            var filterResult = _db.Cars.Where(c => c.Model.Replace(" ","").ToLower().Trim() == model.Replace(" ", "").ToLower().Trim()).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count < 0)
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

        //Search car details by Make name
        public List<CarWithLocationDto> GetCarByMake(string make)
        {
            var filterResult = _db.Cars.Where(c => c.Make.Replace(" ", "").ToLower().Trim() == make.Replace(" ", "").ToLower().Trim()).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count < 0)
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

        //Search car details by Colour name
        public List<CarWithLocationDto> GetCarByColour(string colour)
        {
            var filterResult = _db.Cars.Where(c => c.Colour.Replace(" ", "").ToLower().Trim() == colour.Replace(" ", "").ToLower().Trim()).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count < 0)
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
        //Search car details by Price
        public List<CarWithLocationDto> GetCarByPrice(decimal price)
        {
            var filterResult = _db.Cars.Where(c => c.PricePerDay == price).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count < 0)
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

        //Search car details by Seats

        public List<CarWithLocationDto> GetCarBySeat(int seats)
        {
            var filterResult = _db.Cars.Where(c => c.TotalSeats == seats).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count < 0)
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

        //Search car details by Availability

        public List<CarWithLocationDto> GetCarByAvailability(bool availableSatatus)
        {
            var filterResult = _db.Cars.Where(c => c.AvailableStatus == availableSatatus).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count < 0)
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

        //Search car details by available dates

        public List<CarWithLocationDto> GetCarByAvailableDates(DateOnly availableDate)
        {
            var filterResult = _db.Cars.Where(c => c.AvailableDate == availableDate).ToList();

            List<CarWithLocationDto> result = new List<CarWithLocationDto>();

            if (filterResult.Count < 0)
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

        //Search car details by Price Range
        public List<CarWithRatingsDto> GetCarByPriceRange(decimal maxPrice, decimal minPrice)
        {
            var filterResult = _db.Cars.Where(c => c.PricePerDay >= minPrice && c.PricePerDay <= maxPrice).ToList();

            List<CarWithRatingsDto> result = new List<CarWithRatingsDto>();
            if (filterResult.Count < 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);
                var ratings = _db.Reviews.Where(r => r.CarId == car.CarId);
                if (ratings.Count() == 0)
                {

                    result.Add(new CarWithRatingsDto()
                    {
                        Make = car.Make,
                        Model = car.Model,
                        year = car.year,
                        Colour = car.Colour,
                        TotalSeats = car.TotalSeats,
                        LicensePlate = car.LicensePlate,
                        PricePerDay = car.PricePerDay,
                        imageUrl = car.ImageUrl,
                        //Description = car.Description,
                        AvailableStatus = car.AvailableStatus,
                        AvailableDate = car.AvailableDate,
                        City = locationOfFilterResult.City,
                        Address = locationOfFilterResult.Address,
                        State = locationOfFilterResult.State,
                        Country = locationOfFilterResult.Country,
                        ZipCode = locationOfFilterResult.ZipCode,
                        Rating = 0,
                        TotalRatings = 0

                        //LocationId = car.LocationId
                    });
                }
                else
                {
                    var avgRating = 0;
                    var sumOfAllRatings = 0;
                    var ratingCount = 0;

                    foreach (var item in ratings)
                    {
                        sumOfAllRatings += item.Rating;
                        ratingCount++;
                    }

                    avgRating = sumOfAllRatings / ratings.Count();

                    result.Add(new CarWithRatingsDto()
                    {
                        Make = car.Make,
                        Model = car.Model,
                        year = car.year,
                        Colour = car.Colour,
                        TotalSeats = car.TotalSeats,
                        LicensePlate = car.LicensePlate,
                        PricePerDay = car.PricePerDay,
                        imageUrl = car.ImageUrl,
                        //Description = car.Description,
                        AvailableStatus = car.AvailableStatus,
                        AvailableDate = car.AvailableDate,
                        City = locationOfFilterResult.City,
                        Address = locationOfFilterResult.Address,
                        State = locationOfFilterResult.State,
                        Country = locationOfFilterResult.Country,
                        ZipCode = locationOfFilterResult.ZipCode,
                        Rating = avgRating,
                        TotalRatings = ratingCount,

                        //LocationId = car.LocationId
                    });
                }
             
            }
            return result;
        }


        public List<CarWithRatingsDto> GetCarsByCarFilters(string Colour, int year, int TotalSeats, bool AvailableStatus)
        {
            //var location = _db.Locations.FirstOrDefault(l =>
            //l.City.Replace(" ", "").ToLower().Trim() == City.Replace(" ", "").ToLower().Trim() &&
            // l.State.Replace(" ", "").ToLower().Trim() == State.Replace(" ", "").ToLower().Trim());

            List<CarWithRatingsDto> result = new List<CarWithRatingsDto>();

            //if (location == null)
            //{
            //    return result = new() { };
            //}

            var filterResult = _db.Cars.Where(c =>
            c.Colour.Replace(" ", "").ToLower().Trim() == Colour.Replace(" ", "").ToLower().Trim() && c.TotalSeats == TotalSeats &&
            c.AvailableStatus == AvailableStatus).ToList();


            if (filterResult.Count <= 0)
            {
                return result = new() { };
            }

            foreach (var car in filterResult)
            {
                var locationOfFilterResult = _db.Locations.Find(car.LocationId);
                var ratings = _db.Reviews.Where(r => r.CarId == car.CarId);

                if (ratings.Count() == 0)
                {

                    result.Add(new CarWithRatingsDto()
                    {
                        Make = car.Make,
                        Model = car.Model,
                        year = car.year,
                        Colour = car.Colour,
                        TotalSeats = car.TotalSeats,
                        LicensePlate = car.LicensePlate,
                        PricePerDay = car.PricePerDay,
                        imageUrl = car.ImageUrl,
                        //Description = car.Description,
                        AvailableStatus = car.AvailableStatus,
                        AvailableDate = car.AvailableDate,
                        City = locationOfFilterResult.City,
                        Address = locationOfFilterResult.Address,
                        State = locationOfFilterResult.State,
                        Country = locationOfFilterResult.Country,
                        ZipCode = locationOfFilterResult.ZipCode,
                        Rating = 0,
                        TotalRatings = 0

                        //LocationId = car.LocationId
                    });
                }
                else
                {
                    var avgRating = 0;
                    var sumOfAllRatings = 0;
                    var ratingCount = 0;

                    foreach (var item in ratings)
                    {
                        sumOfAllRatings += item.Rating;
                        ratingCount++;
                    }

                    avgRating = sumOfAllRatings / ratings.Count();

                    result.Add(new CarWithRatingsDto()
                    {
                        Make = car.Make,
                        Model = car.Model,
                        year = car.year,
                        Colour = car.Colour,
                        TotalSeats = car.TotalSeats,
                        LicensePlate = car.LicensePlate,
                        PricePerDay = car.PricePerDay,
                        imageUrl = car.ImageUrl,
                        //Description = car.Description,
                        AvailableStatus = car.AvailableStatus,
                        AvailableDate = car.AvailableDate,
                        City = locationOfFilterResult.City,
                        Address = locationOfFilterResult.Address,
                        State = locationOfFilterResult.State,
                        Country = locationOfFilterResult.Country,
                        ZipCode = locationOfFilterResult.ZipCode,
                        Rating = avgRating,
                        TotalRatings = ratingCount,

                        //LocationId = car.LocationId
                    });
               }           
            }
            return result;
        }
    }
}
