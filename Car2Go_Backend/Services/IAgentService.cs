using Car2Go.DTOs;

namespace Car2Go.Services
{
    public interface IAgentService
    {
        public List<AgentCarWithImageDto> GetCars();
        public AgentCarDto CreateCar(AgentCreateCarDto carDto, string email);
        public bool UpdateCar(AgentUpdateCarDto car, string licensePlate);
        public bool UpdateCarWithLocation(AgentCarWithLocationDto carWithLocationDto, string licensePlate);
        public bool DeleteCar(string licensePlate);

        public List<AllDetailsReservationDto> GetCommonReservationDetails(List<AgentCarWithImageDto> agentCars, List<AllDetailsReservationDto> reservationCars);
        public List<AgentCarWithImageDto> GetCarsByEmail(string email);
    }
}
