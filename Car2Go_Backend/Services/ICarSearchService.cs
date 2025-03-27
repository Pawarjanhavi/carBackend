using Car2Go.DTOs;

namespace Car2Go.Services
{
    public interface ICarSearchService
    {
        List<CarWithRatingsDto> GetAllCarsBySearchValue(string searchValue);
        //List<CarWithImageDto> GetCarsByCarFilters(string City, string State, string Colour, int year, int TotalSeats, bool AvailableStatus, decimal minPrice, decimal maxPrice);
        List<CarWithRatingsDto> GetCarsByCarFilters(string Colour, int year, int TotalSeats, bool AvailableStatus);

        List<CarWithLocationDto> GetCarByModel(string model);
        List<CarWithLocationDto> GetCarByMake(string make);
        List<CarWithLocationDto> GetCarByColour(string colour);
        List<CarWithLocationDto> GetCarByPrice(decimal price);
        List<CarWithLocationDto> GetCarBySeat(int seats);
        List<CarWithLocationDto> GetCarByAvailability(bool availableStatus);
        List<CarWithLocationDto> GetCarByAvailableDates(DateOnly availableDate);
        List<CarWithRatingsDto> GetCarByPriceRange(decimal maxPrice,decimal minPrice);
    }   
}
