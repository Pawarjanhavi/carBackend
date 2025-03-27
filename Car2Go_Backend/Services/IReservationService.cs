using Car2Go.DTOs;

namespace Car2Go.Services
{
    public interface IReservationService
    {
        public int CreateReservation(ReservationRequestDto dto); //done
        public string UpdateReservation(UpdateReservationDto dto, string email,string licensePlate); //done

        //Fetch details of reservation - specific user with specific car
        public List<ReservationDetailsDto> GetReservationDetailsOfUserWithCar(string email,string licensePlate);

        //Fetch details of reservation - specific user with all car( booked or cancelled)
        public List<ReservationDetailsDto> GetReservationHistoryOfUser(string userEmail);

        //Fetch details of all reservation - all user with all car(booked or cancelled)
        public List<AllDetailsReservationDto> GetAllReservation();
        public string CancelReservation(string email,string licensePlate); //done
        public string DeleteReservation(string email); //done
    }
}
