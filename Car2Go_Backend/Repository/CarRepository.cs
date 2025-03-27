using Car2Go.Data;
using Car2Go.DTOs;
using Car2Go.Models;
using Car2Go.Services;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;


namespace Car2Go.Repository
{
    public class CarRepository:ICarService
    {
        Car2GoDBContext _db;
        private readonly Cloudinary _cloudinary;
        public CarRepository(Car2GoDBContext db, IConfiguration configuration)
        {
            _db = db;

            //var cloudinarySettings = configuration.GetSection("Cloudinary");
            //_cloudinary = new Cloudinary(cloudinarySettings.Value);

            var account = new Account(
            configuration["Cloudinary:CloudName"],
            configuration["Cloudinary:ApiKey"],
            configuration["Cloudinary:ApiSecret"]
        );
            _cloudinary = new Cloudinary(account);
        }

        //Image Upload on Cloudinary logic

        public string UploadImage(IFormFile imageFile)
        {
            using (var stream = imageFile.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imageFile.FileName, stream)
                };

                var uploadResult = _cloudinary.Upload(uploadParams);
                return uploadResult.Url.ToString();
            }
        }

        //create new location if location is not already present in db
        public int CreateNewLocation(LocationDto locationDto)
        {
            Location newLocation = new()
            {
                City = locationDto.City,
                Address = locationDto.Address,
                State = locationDto.State,
                Country = locationDto.Country,
                ZipCode = locationDto.ZipCode
            };

            _db.Locations.Add(newLocation);
            _db.SaveChanges();

            var result = _db.Locations.FirstOrDefault(l=>l.Address == locationDto.Address);

            return result.LocationId;
        }

        //Create new car with location(if location is already in db, fetch the id and add in FK(location Id) 
        //If location is not presnt in db, create new location(call CreateNewLocation() function)

        public CarDto CreateCar(CreateCarDto carDto)
        {
            var isLocationPresent = _db.Locations.FirstOrDefault(l => 
            l.Address.Replace(" ", "").ToLower() == carDto.Address.Replace(" ", "").ToLower());

            Car isCarPresent = new Car();

            if (isLocationPresent != null)
            {

                isCarPresent = _db.Cars.FirstOrDefault(c =>
                c.LicensePlate.Replace(" ", "").ToLower().Trim() == carDto.LicensePlate.Replace(" ", "").ToLower().Trim() &&
                c.LocationId == isLocationPresent.LocationId);
            }

            //CarWithLocationDto checkCar = new CarWithLocationDto();

            //Car checkCar = new Car();

            Car newCar = new Car();
            LocationDto newLocation = new LocationDto();

            CarDto result = new CarDto();


            if (isCarPresent != null && isLocationPresent != null)
            {
                if (isCarPresent.LicensePlate.Replace(" ", "").ToLower().Trim() == carDto.LicensePlate.Replace(" ", "").ToLower().Trim()
                && isCarPresent.LocationId == isLocationPresent.LocationId)
                {
                    //checkCar = new()
                    //{
                    //    Make = carDto.Make,
                    //    Model = carDto.Model,
                    //    year = carDto.year,
                    //    Colour = carDto.Colour,
                    //    TotalSeats = carDto.TotalSeats,
                    //    LicensePlate = carDto.LicensePlate,
                    //    PricePerDay = carDto.PricePerDay,
                    //    Description = carDto.Description,
                    //    AvailableStatus = carDto.AvailableStatus,
                    //    AvailableDate = carDto.AvailableDate,
                    //    LocationId = isLocationPresent.LocationId
                    //};
                    ////check car with same location is already present or not, if not then add new one record

                    //var carWithSameLocation = _db.Cars.FirstOrDefault(c => c.LocationId == checkCar.LocationId);

                    //if (carWithSameLocation == checkCar)
                    //{
                    result = new() { };
                    return result;
                    //}
                }
            }

            //get image link 

            string carImageUrl =  UploadImage(carDto.CarImageFile);

            //var image = new Image
            //{
            //    ImageUrl = imageUrl,
            //};

            //_context.Images.Add(image);
            //await _context.SaveChangesAsync();

            //create new record

            //check that location is already present, if it is present fetch that location id
            if (isLocationPresent != null)
                {

                    newCar = new()
                    {
                        Make = carDto.Make,
                        Model = carDto.Model,
                        year = carDto.year,
                        Colour = carDto.Colour,
                        TotalSeats = carDto.TotalSeats,
                        LicensePlate = carDto.LicensePlate,
                        PricePerDay = carDto.PricePerDay,
                        //Description = carDto.Description,
                        ImageUrl = carImageUrl,
                        AvailableStatus = carDto.AvailableStatus,
                        AvailableDate = carDto.AvailableDate,
                        LocationId = isLocationPresent.LocationId
                    };
                }

                //if not then create new location in db and assign that location id to location id(FK)
                else
                {
                    newLocation = new()
                    {
                        City = carDto.City,
                        Address = carDto.Address,
                        State = carDto.State,
                        Country = carDto.Country,
                        ZipCode = carDto.ZipCode
                    };

                    int resultId = CreateNewLocation(newLocation);

                    newCar = new()
                    {
                        Make = carDto.Make,
                        Model = carDto.Model,
                        year = carDto.year,
                        Colour = carDto.Colour,
                        TotalSeats = carDto.TotalSeats,
                        LicensePlate = carDto.LicensePlate,
                        PricePerDay = carDto.PricePerDay,
                        //Description = carDto.Description,
                        ImageUrl = carImageUrl,
                        AvailableStatus = carDto.AvailableStatus,
                        AvailableDate = carDto.AvailableDate,
                        LocationId = resultId
                    };
                }
                result = new()
                {
                    Make = newCar.Make,
                    Model = newCar.Model,
                    year = newCar.year,
                    Colour = newCar.Colour,
                    TotalSeats = newCar.TotalSeats,
                    LicensePlate = newCar.LicensePlate,
                    PricePerDay = newCar.PricePerDay,
                    //Description = newCar.Description,
                    AvailableStatus = newCar.AvailableStatus,
                    AvailableDate = newCar.AvailableDate,
                    imageUrl = newCar.ImageUrl,
                    LocationId = newCar.LocationId
                };
                _db.Cars.Add(newCar);
                _db.SaveChanges();

            //} 
            return result;
        }

        //Get all cars (available or not available)
        public List<CarWithImageDto> GetCars()
        {
            List<Car> cars;
            List<CarWithImageDto> carList= new List<CarWithImageDto>();
            try
            {
                cars = _db.Cars.ToList();
                foreach (Car car in cars)
                {
                    var location = _db.Locations.FirstOrDefault(l => l.LocationId == car.LocationId);

                    carList.Add(new CarWithImageDto()
                    {
                      Make = car.Make,
                      Model = car.Model,
                      year = car.year,
                      Colour = car.Colour,
                      TotalSeats = car.TotalSeats,
                      LicensePlate = car.LicensePlate,
                      PricePerDay = car.PricePerDay,
                      //Description = car.Description,
                      imageUrl = car.ImageUrl,
                      AvailableStatus = car.AvailableStatus,
                      AvailableDate = car.AvailableDate,
                      City = location.City,
                      Address = location.Address,
                      State = location.State,
                      Country = location.Country,
                      ZipCode = location.ZipCode

                      //LocationId = car.LocationId
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return carList;
        }

        //Update Car details only
        public bool UpdateCar(UpdateCarDto carDto, string licencePlate)
        {
            var existingCar = _db.Cars.FirstOrDefault(c=>c.LicensePlate.Replace(" ", "").ToLower().Trim() == licencePlate.Replace(" ", "").ToLower().Trim());

            try
            {
                if (carDto == null || existingCar == null)
                {
                    return false;
                }
                existingCar.Make = carDto.Make;
                existingCar.Model = carDto.Model;
                existingCar.year = carDto.year;
                existingCar.Colour = carDto.Colour;
                existingCar.TotalSeats = carDto.TotalSeats;
                existingCar.LicensePlate = carDto.LicensePlate;
                existingCar.PricePerDay = carDto.PricePerDay;
                //existingCar.Description = carDto.Description;
                existingCar.AvailableStatus = carDto.AvailableStatus;
                existingCar.AvailableDate = carDto.AvailableDate;

                _db.Cars.Update(existingCar);
                _db.SaveChanges();
            }
            catch (Exception e) { throw e; }
            return true;
        }

        //Update Car details with Location details

        public bool UpdateCarWithLocation(CarWithLocationDto carWithLocationDto, string licencePlate)
        {
            LocationDto newLocation = new LocationDto();
            int newLocationId = 0;
            Location existingLocation = null;
            Location getNewLocation = new Location();


            if (carWithLocationDto == null)
            {
                return false;
            }
            var existingCar = _db.Cars.FirstOrDefault(c => c.LicensePlate.Replace(" ", "").ToLower().Trim() == licencePlate.Replace(" ", "").ToLower().Trim());
            if (existingCar == null)
            {
                return false;
            }
            else
            {
                existingLocation = _db.Locations.FirstOrDefault(l =>
                l.Address.Replace(" ", "").ToLower().Trim() == carWithLocationDto.Address.Replace(" ", "").ToLower().Trim());

                if (existingLocation != null)
                {
                    newLocationId = existingLocation.LocationId;

                    existingCar.Make = carWithLocationDto.Make;
                    existingCar.Model = carWithLocationDto.Model;
                    existingCar.year = carWithLocationDto.year;
                    existingCar.Colour = carWithLocationDto.Colour;
                    existingCar.TotalSeats = carWithLocationDto.TotalSeats;
                    existingCar.LicensePlate = carWithLocationDto.LicensePlate;
                    existingCar.PricePerDay = carWithLocationDto.PricePerDay;
                    //existingCar.Description = carWithLocationDto.Description;
                    existingCar.AvailableStatus = carWithLocationDto.AvailableStatus;
                    existingCar.AvailableDate = carWithLocationDto.AvailableDate;

                    existingLocation.Address = existingLocation.Address;
                    existingLocation.City = existingLocation.City;
                    existingLocation.Country = existingLocation.Country;
                    existingLocation.State = existingLocation.State;
                    existingLocation.ZipCode = existingLocation.ZipCode;
                }
                else
                {
                    //create new location in db
                    newLocation = new()
                    {
                        City = carWithLocationDto.City,
                        Address = carWithLocationDto.Address,
                        State = carWithLocationDto.State,
                        Country = carWithLocationDto.Country,
                        ZipCode = carWithLocationDto.ZipCode
                    };
                    //get location id of newly created record
                    newLocationId = CreateNewLocation(newLocation);

                    getNewLocation = _db.Locations.FirstOrDefault(l=>l.LocationId==newLocationId);

                    existingCar.Make = carWithLocationDto.Make;
                    existingCar.Model = carWithLocationDto.Model;
                    existingCar.year = carWithLocationDto.year;
                    existingCar.Colour = carWithLocationDto.Colour;
                    existingCar.TotalSeats = carWithLocationDto.TotalSeats;
                    existingCar.LicensePlate = carWithLocationDto.LicensePlate;
                    existingCar.PricePerDay = carWithLocationDto.PricePerDay;
                    //existingCar.Description = carWithLocationDto.Description;
                    existingCar.AvailableStatus = carWithLocationDto.AvailableStatus;
                    existingCar.AvailableDate = carWithLocationDto.AvailableDate;
                    existingCar.LocationId = newLocationId;

                    //existingLocation.Address = getNewLocation.Address;
                    //existingLocation.City = getNewLocation.City;
                    //existingLocation.Country = getNewLocation.Country;
                    //existingLocation.State = getNewLocation.State;
                    //existingLocation.ZipCode = getNewLocation.ZipCode;
                }
            }

           

          

            //Car existingCar=null;

            //Location checkLocation = new Location();

            ////check location is present, if not it means on that location no car is present

            //if (existingLocation == null)
            //{
            //    existingCar = _db.Cars.FirstOrDefault(c => c.LicensePlate.Replace(" ", "").ToLower().Trim() == licencePlate.Replace(" ", "").ToLower().Trim() && c.LocationId == existingLocation.LocationId);
            //    if (existingCar == null)
            //    {
            //        return false; //car not present
            //    }
            //    else
            //    {     
            //        if (existingLocation == null)
            //        {
            //            //create new location in db
            //            newLocation = new()
            //            {
            //                City = carWithLocationDto.City,
            //                Address = carWithLocationDto.Address,
            //                State = carWithLocationDto.State,
            //                Country = carWithLocationDto.Country,
            //                ZipCode = carWithLocationDto.ZipCode
            //            };
            //            //get location id of newly created record
            //            newLocationId = CreateNewLocation(newLocation);
            //        }
            //        //fetch the location of newly created record by id
            //        checkLocation = _db.Locations.FirstOrDefault(l => l.LocationId == newLocationId);

            //        existingCar.Make = carWithLocationDto.Make;
            //        existingCar.Model = carWithLocationDto.Model;
            //        existingCar.year = carWithLocationDto.year;
            //        existingCar.Colour = carWithLocationDto.Colour;
            //        existingCar.TotalSeats = carWithLocationDto.TotalSeats;
            //        existingCar.LicensePlate = carWithLocationDto.LicensePlate;
            //        existingCar.PricePerDay = carWithLocationDto.PricePerDay;
            //        //existingCar.Description = carWithLocationDto.Description;
            //        existingCar.AvailableStatus = carWithLocationDto.AvailableStatus;
            //        existingCar.AvailableDate = carWithLocationDto.AvailableDate;

            //        existingLocation.Address = carWithLocationDto.Address;
            //        existingLocation.City = carWithLocationDto.City;
            //        existingLocation.Country = carWithLocationDto.Country;
            //        existingLocation.State = carWithLocationDto.State;
            //        existingLocation.ZipCode = carWithLocationDto.ZipCode;
            //    }
            //}

            //LocationDto newLocation = new LocationDto();
            //int newLocationId = 0;
            //if (existingLocation != null)
            //{
            //    newLocation = new()
            //    {
            //        City = carWithLocationDto.City,
            //        Address = carWithLocationDto.Address,
            //        State = carWithLocationDto.State,
            //        Country = carWithLocationDto.Country,
            //        ZipCode = carWithLocationDto.ZipCode
            //    };

            //    newLocationId = CreateNewLocation(newLocation);
            //}

            //var location = _db.Locations.FirstOrDefault(l => l.LocationId == newLocationId);

            //existingCar.Make = carWithLocationDto.Make;
            //existingCar.Model = carWithLocationDto.Model;
            //existingCar.year = carWithLocationDto.year;
            //existingCar.Colour = carWithLocationDto.Colour;
            //existingCar.TotalSeats = carWithLocationDto.TotalSeats;
            //existingCar.LicensePlate = carWithLocationDto.LicensePlate;
            //existingCar.PricePerDay = carWithLocationDto.PricePerDay;
            //existingCar.Description = carWithLocationDto.Description;
            //existingCar.AvailableStatus = carWithLocationDto.AvailableStatus;
            //existingCar.AvailableDate = carWithLocationDto.AvailableDate;

            //existingLocation.Address = carWithLocationDto.Address;
            //existingLocation.City = carWithLocationDto.City;
            //existingLocation.Country = carWithLocationDto.Country;
            //existingLocation.State = carWithLocationDto.State;
            //existingLocation.ZipCode = carWithLocationDto.ZipCode;

            _db.Cars.Update(existingCar);
            //_db.Locations.Update(existingLocation);
            _db.SaveChanges();
            return true;
        }

        //Delete Car details only
        public bool DeleteCar(string licensePlate)
        {
            var existingCar = _db.Cars.FirstOrDefault(c => c.LicensePlate.Replace(" ", "").ToLower().Trim() == licensePlate.Replace(" ", "").ToLower().Trim());

            try
            {
                if (licensePlate == null || existingCar == null) { return false; }
                _db.Cars.Remove(existingCar);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        //Get all cars (with rating)
        public List<CarWithRatingsDto> GetCarsWithRating()
        {
            List<Car> cars;
            List<CarWithRatingsDto> carList = new List<CarWithRatingsDto>();
            try
            {
                cars = _db.Cars.ToList();
                foreach (Car car in cars)
                {
                    var location = _db.Locations.FirstOrDefault(l => l.LocationId == car.LocationId);

                    var ratings = _db.Reviews.Where(r => r.CarId == car.CarId);

                    if (ratings.Count() == 0)
                    {

                        carList.Add(new CarWithRatingsDto()
                        {
                            Make = car.Make,
                            Model = car.Model,
                            year = car.year,
                            Colour = car.Colour,
                            TotalSeats = car.TotalSeats,
                            LicensePlate = car.LicensePlate,
                            PricePerDay = car.PricePerDay,
                            //Description = car.Description,
                            imageUrl = car.ImageUrl,
                            AvailableStatus = car.AvailableStatus,
                            AvailableDate = car.AvailableDate,
                            City = location.City,
                            Address = location.Address,
                            State = location.State,
                            Country = location.Country,
                            ZipCode = location.ZipCode,
                            Rating = 0,
                            TotalRatings=0

                            //LocationId = car.LocationId
                        });
                    }
                    else
                    {
                        var avgRating=0;
                        var sumOfAllRatings = 0;
                        var ratingCount = 0;

                        foreach (var item in ratings)
                        {
                            sumOfAllRatings += item.Rating;
                            ratingCount++;
                        }

                        avgRating = sumOfAllRatings / ratings.Count();

                        carList.Add(new CarWithRatingsDto()
                        {
                            Make = car.Make,
                            Model = car.Model,
                            year = car.year,
                            Colour = car.Colour,
                            TotalSeats = car.TotalSeats,
                            LicensePlate = car.LicensePlate,
                            PricePerDay = car.PricePerDay,
                            //Description = car.Description,
                            imageUrl = car.ImageUrl,
                            AvailableStatus = car.AvailableStatus,
                            AvailableDate = car.AvailableDate,
                            City = location.City,
                            Address = location.Address,
                            State = location.State,
                            Country = location.Country,
                            ZipCode = location.ZipCode,
                            Rating = avgRating,
                            TotalRatings=ratingCount,

                            //LocationId = car.LocationId
                        });
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return carList;
        }
    }
}
