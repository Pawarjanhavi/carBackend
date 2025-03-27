using Car2Go.DTOs;

namespace Car2Go.Services
{
    public interface ICarFilterService
    {
        List<CarWithLocationDto> GetCarsByModelAndMake(string make,string model);
        List<CarWithLocationDto> GetCarsByMakeAndColour(string make, string colour);
        List<CarWithLocationDto> GetCarsByMakeAndPrice(string make, decimal price);
        List<CarWithLocationDto> GetCarsByMakeAndSeats(string make, int seats);
        List<CarWithLocationDto> GetCarsByMakeAndAvailability(string make, bool availableStatus);
        List<CarWithLocationDto> GetCarsByMakeAndAvailableDates(string make, DateOnly availableDate);

        List<CarWithLocationDto> GetCarsByModelAndPrice(string model, decimal price);
        List<CarWithLocationDto> GetCarsByModelAndSeats(string model, int seats);
        List<CarWithLocationDto> GetCarsByModelAndAvailability(string model, bool availableStatus);
        List<CarWithLocationDto> GetCarsByModelAndAvailableDates(string model, DateOnly availableDate);

        List<CarWithLocationDto> GetCarsByColourAndPrice(string colour, decimal price);
        List<CarWithLocationDto> GetCarsByColourAndSeats(string colour, int seats);
        List<CarWithLocationDto> GetCarsByColourAndAvailabilty(string colour, bool availableStatus);
        List<CarWithLocationDto> GetCarsByColourAndAvailableDates(string colour, DateOnly availableDate);


        List<CarWithLocationDto> GetCarsByPriceAndSeats(decimal price,int seats);
        List<CarWithLocationDto> GetCarsByPriceAndAvailability(decimal price, bool availableStatus);
        List<CarWithLocationDto> GetCarsByPriceAndAvailableDates(decimal price, DateOnly availableDate);

        List<CarWithLocationDto> GetCarsBySeatsAndAvailability(int seats, bool availableStatus);
        List<CarWithLocationDto> GetCarsBySeatsAndAvailableDates(int seats, DateOnly availableDate);

       public List<CarWithLocationDto> GetCarsByAvailabilityAndAvailableDates(bool availableStatus, DateOnly availableDate);

        List<CarWithLocationDto> GetCarsByAllCarFilters(string model, string make,string colour, decimal price, int seats, bool availableStatus, DateOnly availableDate);

    }
}
