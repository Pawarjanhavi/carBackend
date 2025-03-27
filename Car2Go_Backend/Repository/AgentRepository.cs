using Car2Go.Data;
using Car2Go.DTOs;
using Car2Go.Models;
using Car2Go.Services;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace Car2Go.Repository
{
    public class AgentRepository : IAgentService
    {
        Car2GoDBContext _db;
        private readonly Cloudinary _cloudinary;
        public AgentRepository(Car2GoDBContext db, IConfiguration configuration)
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

            var result = _db.Locations.FirstOrDefault(l => l.Address == locationDto.Address);

            return result.LocationId;
        }

        //Create new car with location(if location is already in db, fetch the id and add in FK(location Id) 
        //If location is not presnt in db, create new location(call CreateNewLocation() function)

        public AgentCarDto CreateCar(AgentCreateCarDto carDto, string email)
        {
            var agentPresent = _db.Users.FirstOrDefault(u =>
            u.Email.Replace(" ", "").ToLower().Trim() == email.Replace(" ", "").ToLower().Trim());

            var isLocationPresent = _db.Locations.FirstOrDefault(l =>
            l.Address.Replace(" ", "").ToLower() == carDto.Address.Replace(" ", "").ToLower());

            AgentCar isCarPresent = new AgentCar();

            if (isLocationPresent != null)
            {

                isCarPresent = _db.AgentCars.FirstOrDefault(c =>
                c.LicensePlate.Replace(" ", "").ToLower().Trim() == carDto.LicensePlate.Replace(" ", "").ToLower().Trim() &&
                c.LocationId == isLocationPresent.LocationId);
            }

            AgentCar newCar = new AgentCar();

            LocationDto newLocation = new LocationDto();

            AgentCarDto result = new AgentCarDto();


            if (isCarPresent != null && isLocationPresent != null)
            {
                if (isCarPresent.LicensePlate.Replace(" ", "").ToLower().Trim() == carDto.LicensePlate.Replace(" ", "").ToLower().Trim()
                && isCarPresent.LocationId == isLocationPresent.LocationId)
                {
                    result = new() { };
                    return result;
                }
            }

            //get image link 
            string carImageUrl = UploadImage(carDto.CarImageFile);

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
                    LocationId = isLocationPresent.LocationId,
                    AgentId = agentPresent.UserId
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
            _db.AgentCars.Add(newCar);
            _db.SaveChanges();

            //} 
            return result;
        }

        //Get all cars (available or not available)
        public List<AgentCarWithImageDto> GetCars()
        {
            List<AgentCar> cars;
            List<AgentCarWithImageDto> carList = new List<AgentCarWithImageDto>();
            try
            {
                cars = _db.AgentCars.ToList();
                foreach (AgentCar car in cars)
                {
                    var location = _db.Locations.FirstOrDefault(l => l.LocationId == car.LocationId);

                    carList.Add(new AgentCarWithImageDto()
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
        public bool UpdateCar(AgentUpdateCarDto carDto, string licencePlate)
        {
            var existingCar = _db.AgentCars.FirstOrDefault(c => c.LicensePlate.Replace(" ", "").ToLower().Trim() == licencePlate.Replace(" ", "").ToLower().Trim());

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

                _db.AgentCars.Update(existingCar);
                _db.SaveChanges();
            }
            catch (Exception e) { throw e; }
            return true;
        }

        //Update Car details with Location details

        public bool UpdateCarWithLocation(AgentCarWithLocationDto carWithLocationDto, string licencePlate)
        {
            if (carWithLocationDto == null)
            {
                return false;
            }

            LocationDto newLocation = new LocationDto();
            int newLocationId = 0;

            var existingLocation = _db.Locations.FirstOrDefault(l => l.Address.Replace(" ", "").ToLower().Trim() == carWithLocationDto.Address.Replace(" ", "").ToLower().Trim());

            AgentCar existingCar = null;

            Location checkLocation = new Location();

            //check location is present, if not it means on that location no car is present

            if (existingLocation != null)
            {
                existingCar = _db.AgentCars.FirstOrDefault(c => c.LicensePlate.Replace(" ", "").ToLower().Trim() == licencePlate.Replace(" ", "").ToLower().Trim() && c.LocationId == existingLocation.LocationId);
                if (existingCar == null)
                {
                    return false; //car not present
                }
                else
                {
                    if (existingLocation == null)
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
                    }
                    //fetch the location of newly created record by id
                    checkLocation = _db.Locations.FirstOrDefault(l => l.LocationId == newLocationId);

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

                    existingLocation.Address = carWithLocationDto.Address;
                    existingLocation.City = carWithLocationDto.City;
                    existingLocation.Country = carWithLocationDto.Country;
                    existingLocation.State = carWithLocationDto.State;
                    existingLocation.ZipCode = carWithLocationDto.ZipCode;
                }
            }

            _db.AgentCars.Update(existingCar);
            _db.Locations.Update(existingLocation);
            _db.SaveChanges();
            return true;
        }

        //Delete Car details only
        public bool DeleteCar(string licensePlate)
        {
            var existingCar = _db.AgentCars.FirstOrDefault(c => c.LicensePlate.Replace(" ", "").ToLower().Trim() == licensePlate.Replace(" ", "").ToLower().Trim());

            try
            {
                if (licensePlate == null || existingCar == null) { return false; }
                _db.AgentCars.Remove(existingCar);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public List<AllDetailsReservationDto> GetCommonReservationDetails(List<AgentCarWithImageDto> agentCars, List<AllDetailsReservationDto> reservationCars)
        {
            var commonReservations = new List<AllDetailsReservationDto>();

            foreach (var agentCar in agentCars)
            {
                // Filter reservation cars where the CarNumber matches the LicensePlate
                var matchingReservations = reservationCars
                    .Where(r => r.CarNumber == agentCar.LicensePlate)
                    .ToList();

                // Add all matching reservations to the result list
                commonReservations.AddRange(matchingReservations);
            }

            return commonReservations;
        }
        public List<AgentCarWithImageDto> GetCarsByEmail(string email)
        {
            List<AgentCar> cars;
            List<AgentCarWithImageDto> carList = new List<AgentCarWithImageDto>();

            try
            {
                // Fetch the agent using the sanitized email
                var agent = _db.Users
                    .FirstOrDefault(u => u.Email.Replace(" ", "").ToLower().Trim() == email.Replace(" ", "").ToLower().Trim());

                if (agent == null)
                {
                    return carList; // Return an empty list if the agent is not found
                }


                cars = _db.AgentCars.Where(car => car.AgentId == agent.UserId).ToList();

                foreach (AgentCar car in cars)
                {
                    // Fetch location details for the car
                    var location = _db.Locations.FirstOrDefault(l => l.LocationId == car.LocationId);

                    carList.Add(new AgentCarWithImageDto()
                    {
                        Make = car.Make,
                        Model = car.Model,
                        year = car.year,
                        Colour = car.Colour,
                        TotalSeats = car.TotalSeats,
                        LicensePlate = car.LicensePlate,
                        PricePerDay = car.PricePerDay,
                        imageUrl = car.ImageUrl,
                        AvailableStatus = car.AvailableStatus,
                        AvailableDate = car.AvailableDate,
                        City = location?.City ?? string.Empty,
                        Address = location?.Address ?? string.Empty,
                        State = location?.State ?? string.Empty,
                        Country = location?.Country ?? string.Empty,
                        ZipCode = location?.ZipCode ?? string.Empty
                    });
                }
            }
            catch (Exception e)
            {
                throw e; // Log exception properly in production
            }

            return carList;
        }
    }
}
