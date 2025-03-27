using Car2Go.DTOs;

namespace Car2Go.Services
{
    public interface ICarFilterByLocationService
    {
        //Search and filter by city or state
        List<CarWithLocationDto> GetCarByCity(string city);
        List<CarWithLocationDto> GetCarByState(string state);

        //filter by both city and state
        List<CarWithLocationDto> GetCarByCityAndState(string city,string state);

    }
}
