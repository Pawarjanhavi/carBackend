using Car2Go.Data;
using Car2Go.DTOs;
using Car2Go.Models;
using Car2Go.Services;
using System.Net;

namespace Car2Go.Repository
{
    public class LocationRepository:ILocationService
    {
        private Car2GoDBContext _dbContext;
        public LocationRepository(Car2GoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all locations
        public List<LocationDto> GetLocations()
        {
            List<Location> locations;

            List<LocationDto> locationList = new List<LocationDto>();
            try
            {
                locations = _dbContext.Locations.ToList();

                foreach (Location location in locations)
                {
                    locationList.Add(new LocationDto()
                    {
                        City = location.City,
                        Address = location.Address,
                        State = location.State,
                        Country = location.Country,
                        ZipCode = location.ZipCode,
                    });
                }

                return locationList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Create new Location
        public LocationDto CreateLocation(LocationDto locationDto)
        {
            var locationExist = _dbContext.Locations.FirstOrDefault(l => l.Address.Replace(" ", "").ToLower().Trim() == locationDto.Address.Replace(" ", "").ToLower().Trim());

            LocationDto result;

            try
            {
                if (locationExist != null)
                {
                    //result = new() { };
                    return result=null;
                }
                //if (locationExist != null && locationDto != null)
                //{
                //    LocationDto check = new()
                //    {
                //        City = locationExist.City,
                //        Address = locationExist.Address,
                //        State = locationExist.State,
                //        Country = locationExist.Country,
                //        ZipCode = locationExist.ZipCode
                //    };

                //    if (locationDto == check)
                //    {
                //        result = new() { };
                //        return result;
                //    }
                //}

                if (locationDto == null)
                {
                    //result = new() { };
                    return result=null;
                }

                Location newLocation = new()
                {
                    City = locationDto.City,
                    Address = locationDto.Address,
                    State = locationDto.State,
                    Country = locationDto.Country,
                    ZipCode = locationDto.ZipCode
                };

                result = new()
                {
                    City = newLocation.City,
                    Address = newLocation.Address,
                    State = newLocation.State,
                    Country = newLocation.Country,
                    ZipCode = newLocation.ZipCode
                };


                _dbContext.Locations.Add(newLocation);
                _dbContext.SaveChanges();
                return result;
            }
            catch (Exception ex) { throw ex; }
        }

        //Update existing Location
        public bool UpdateLocation(string address, LocationDto locationDto)
        {
            var existingLocation = _dbContext.Locations.FirstOrDefault(l => l.Address.Replace(" ", "").ToLower().Trim() == address.Replace(" ", "").ToLower().Trim());

            try
            {
                if (locationDto == null || existingLocation == null)
                {
                    return false;
                }

                existingLocation.City = locationDto.City;
                existingLocation.Address = locationDto.Address;
                existingLocation.State = locationDto.State;
                existingLocation.Country = locationDto.Country;
                existingLocation.ZipCode = locationDto.ZipCode;

                _dbContext.Locations.Update(existingLocation);
                _dbContext.SaveChanges();
                return true;
            }catch(Exception ex) { throw ex; }
        }

        //Delete Location(Do it if necessary)
        public bool DeleteLocation(string address)
        {
            var existingLocation = _dbContext.Locations.FirstOrDefault(l => l.Address.Replace(" ", "").ToLower().Trim() == address.Replace(" ", "").ToLower().Trim());
            try
            {
                if (existingLocation == null)
                {
                    return false;
                }
                _dbContext.Remove(existingLocation);
                _dbContext.SaveChanges();
                return true;
            }catch(Exception ex) { throw ex; }
        }
    }
}
