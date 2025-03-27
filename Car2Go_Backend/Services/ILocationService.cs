using Car2Go.DTOs;
using Car2Go.Models;

namespace Car2Go.Services
{
    public interface ILocationService
    {
        public List<LocationDto> GetLocations();
        public LocationDto CreateLocation(LocationDto locationDto);
        public bool UpdateLocation(string address,LocationDto locationDto);
        public bool DeleteLocation(string address);
    }
}
