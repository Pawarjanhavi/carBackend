using Car2Go.Data;
using Car2Go.DTOs;
using Car2Go.Models;
using Car2Go.Services;
using System.Linq;

namespace Car2Go.Repository
{
    public class ReservationRepository:IReservationService
    {
        private readonly Car2GoDBContext _dbContext;

        public ReservationRepository(Car2GoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public string CreateReservation(ReservationRequestDto dto)
        //{
        //    try
        //    {
        //        // Find the user by email
        //        var user = _dbContext.Users.FirstOrDefault(u =>
        //        u.Email.Replace(" ", "").ToLower().Trim() == dto.Email.Replace(" ", "").ToLower().Trim());

        //        // Find the car by make and availability status
        //        var car = _dbContext.Cars.FirstOrDefault(c => 
        //        c.LicensePlate.Replace(" ","").ToLower().Trim() == dto.LicensePlate.Replace(" ", "").ToLower().Trim() &&
        //        c.AvailableStatus==true);

        //        if (user == null && car==null)
        //        {
        //            return "User and Car not found"; 
        //        }
        //        else if (car == null)
        //        {
        //            return "Car not available"; // Return message if car does not exist or is not available
        //        }
        //        else if (user == null)
        //        {
        //            return "User not found"; // Return message if user does not exist
        //        }

        //        // Ensure pickUpDate and dropOffDate are non-null and valid
        //        DateOnly pickUpDate = dto.PickUpDate;
        //        DateOnly dropOffDate = dto.DropOffDate;

        //        //var check = pickUpDate.AddDays(2);

        //        if (pickUpDate != car.AvailableDate)
        //        {
        //            return "Pickup date must be same as available date";
        //        }

        //        //if (dropOffDate <= pickUpDate || dropOffDate >= check)
        //        if (dropOffDate <= pickUpDate)
        //        {
        //            return "Invalid reservation: Drop-off date must be after the pick-up date."; // Invalid date range
        //        }

        //        // Calculate rental duration in days
        //        //TimeSpan rentalDuration = dropOffDate.Day - pickUpDate.Day;

        //        var check = pickUpDate.AddDays(1);
        //        decimal amount;

        //        if (check == dropOffDate)
        //        {
        //            amount = car.PricePerDay;
        //        }
        //        else
        //        {
        //            var rentalDuration = dropOffDate.DayNumber - pickUpDate.DayNumber;
        //            amount = rentalDuration * car.PricePerDay;

        //        }

        //        // Calculate the reservation amount based on rental duration
        //        //var amount = (decimal)rentalDuration.TotalDays * car.PricePerDay;

        //        //var amount = car.PricePerDay;

        //        // Create the reservation entity
        //        var reservation = new Reservation
        //        {
        //            UserId = user.UserId,
        //            CarId = car.CarId,
        //            PickUpDate = pickUpDate,
        //            DropOffDate = dropOffDate,
        //            Amount = amount,
        //            Status = Status.Confirmed,
        //        };

        //        // Add reservation to the database
        //        _dbContext.Reservations.Add(reservation);

        //        // Update the car availability status to false (indicating it is reserved)
        //        if (reservation.Status == Status.Confirmed)
        //        {
        //            car.AvailableStatus = false;
        //            car.AvailableDate = dropOffDate;
        //        }

        //        // Save changes to the database
        //        _dbContext.SaveChanges();



        //        return "Reservation created successfully."; // Return success message
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (or return the error message)
        //        return $"Error during reservation creation: {ex.Message}";
        //    }
        //}

        public int CreateReservation(ReservationRequestDto dto)
        {
            try
            {
                // Find the user by email
                var user = _dbContext.Users.FirstOrDefault(u =>
                    u.Email.Replace(" ", "").ToLower().Trim() == dto.Email.Replace(" ", "").ToLower().Trim());

                // Find the car by license plate and availability status
                var car = _dbContext.Cars.FirstOrDefault(c =>
                    c.LicensePlate.Replace(" ", "").ToLower().Trim() == dto.LicensePlate.Replace(" ", "").ToLower().Trim() &&
                    c.AvailableStatus == true);

                if (user == null && car == null)
                {
                    throw new Exception("User and Car not found");
                }
                else if (car == null)
                {
                    throw new Exception("Car not available"); // Return message if car does not exist or is not available
                }
                else if (user == null)
                {
                    throw new Exception("User not found"); // Return message if user does not exist
                }

                // Ensure pickUpDate and dropOffDate are valid
                DateOnly pickUpDate = dto.PickUpDate;
                DateOnly dropOffDate = dto.DropOffDate;

                if (pickUpDate != car.AvailableDate)
                {
                    throw new Exception("Pickup date must be the same as available date");
                }

                if (dropOffDate <= pickUpDate)
                {
                    throw new Exception("Invalid reservation: Drop-off date must be after the pick-up date.");
                }

                // Calculate the reservation amount
                var check = pickUpDate.AddDays(1);
                decimal amount;

                if (check == dropOffDate)
                {
                    amount = car.PricePerDay;
                }
                else
                {
                    var rentalDuration = dropOffDate.DayNumber - pickUpDate.DayNumber;
                    amount = rentalDuration * car.PricePerDay;
                }

                // Create the reservation entity
                var reservation = new Reservation
                {
                    UserId = user.UserId,
                    CarId = car.CarId,
                    PickUpDate = pickUpDate,
                    DropOffDate = dropOffDate,
                    Amount = amount,
                    Status = Status.Confirmed,
                };

                // Add reservation to the database
                _dbContext.Reservations.Add(reservation);

                // Update car availability status
                car.AvailableStatus = false;
                car.AvailableDate = dropOffDate;

                // Save changes to the database
                _dbContext.SaveChanges();

                // Return the ReservationId
                return reservation.ReservationId;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it
                throw new Exception($"Error during reservation creation: {ex.Message}");
            }
        }


        public string UpdateReservation(UpdateReservationDto dto,string email,string licensePlate)
        {
            var user = _dbContext.Users.FirstOrDefault(u =>
            u.Email.Replace(" ", "").ToLower().Trim() == email.Replace(" ", "").ToLower().Trim());

            var car = _dbContext.Cars.FirstOrDefault(c =>
            c.LicensePlate.Replace(" ", "").ToLower().Trim() == licensePlate.Replace(" ", "").ToLower().Trim());

            if (user == null)
            {
                return "User not found.";
            }

            else if (car == null )
            {
                return "Car not found.";
            }
            else if(user==null && car==null)
            {
                return "User and Car not found.";
            }

            var reservation = _dbContext.Reservations.FirstOrDefault(r => 
            r.UserId == user.UserId && r.CarId==car.CarId); // Assuming UserId is a foreign key in Reservation table

            if (reservation == null)
            {
                return "Reservation not found.";
            }

            //var car = _dbContext.Cars.FirstOrDefault(c => c.CarId == reservation.CarId);

            var check = dto.PickUpDate.AddDays(2);

            if(dto.PickUpDate != car.AvailableDate)
            {
                return "Pickup date must be same as available date";
            }


            // Check if the pickUpDate and dropOffDate are valid
            //if (dto.PickUpDate != null && dto.DropOffDate != null && dto.PickUpDate < dto.DropOffDate && dto.DropOffDate < check)
            
            if (dto.PickUpDate != null && dto.DropOffDate != null && dto.PickUpDate < dto.DropOffDate)
            {
                reservation.PickUpDate = dto.PickUpDate;
                reservation.DropOffDate = dto.DropOffDate;

                car.AvailableDate = dto.DropOffDate;

                // Save the changes to the database
                _dbContext.SaveChanges();

                return "Reservation updated successfully.";
            }

            return "Invalid dates: Drop-off date must be after pick-up date.";
        }

        public List<ReservationDetailsDto> GetReservationDetailsOfUserWithCar(string email, string licensePlate)
        {
            try
            {
                // Fetch the user based on the provided email
                var user = _dbContext.Users
                    .FirstOrDefault(u => u.Email == email);

                //fetch the car based on license plate number
                var car = _dbContext.Cars.FirstOrDefault(c =>
                    c.LicensePlate.Replace(" ", "").ToLower().Trim() == licensePlate.Replace(" ", "").ToLower().Trim());

                if (user == null)
                {
                    throw new Exception("User not found.");
                }
                else if (car == null)
                {
                    throw new Exception("Car details not found");
                }
                else if (user == null && car == null)
                {
                    throw new Exception("User and Car details not found");
                }

                // Fetch the reservation based on the user ID
                var reservation = _dbContext.Reservations
                    .Where(r => r.UserId == user.UserId && r.CarId==car.CarId).ToList();

                if (reservation.Count() <= 0)
                {
                    throw new Exception("Reservation not found for this user and car");
                }

                List<ReservationDetailsDto> reservationList = new List<ReservationDetailsDto>();

                foreach (Reservation r in reservation)
                {
                    reservationList.Add(new ReservationDetailsDto()
                    {
                        UserEmail = user.Email,  // User's email associated with the reservation
                        CarMake = car.Make,     // Car make
                        CarModel = car.Model,   // Car model
                        PickUpDate = r.PickUpDate,
                        DropOffDate = r.DropOffDate,
                        TotalAmount = r.Amount,
                    });
                }
                return reservationList;
            }
            catch (Exception ex)
            {
                // Handle errors and return the exception message
                throw new Exception($"Error fetching reservation details: {ex.Message}");
            }
        }

        public string DeleteReservation(string email)
        {
            // Fetch the user based on the email
            var user = _dbContext.Users.FirstOrDefault(u => 
            u.Email.Replace(" ", "").ToLower().Trim() == email.Replace(" ", "").ToLower().Trim());

            if (user == null)
            {
                return "User not found.";  // Handle case where the user does not exist
            }

            // Fetch the reservation based on the user ID
            var reservation = _dbContext.Reservations
                .FirstOrDefault(r => r.UserId == user.UserId);  // Match with the user ID

            if (reservation == null)
            {
                return "Reservation not found.";  // Handle case where reservation doesn't exist
            }

            // Remove the reservation from the database
            _dbContext.Reservations.Remove(reservation);
            _dbContext.SaveChanges();

            return "Reservation deleted successfully.";  // Return success message
        }

        public string CancelReservation(string email,string licensePlate)
        {
            // Fetch the user based on the email
            var user = _dbContext.Users.FirstOrDefault(u => 
            u.Email.Replace(" ", "").ToLower().Trim() == email.Replace(" ", "").ToLower().Trim());

            if (user == null)
            {
                return "User not found."; // Handle case where the user does not exist
            }
            var car = _dbContext.Cars.FirstOrDefault(c=>
            c.LicensePlate.Replace(" ", "").ToLower().Trim() == licensePlate.Replace(" ", "").ToLower().Trim());

            // Find the reservation for this user
            var reservation = _dbContext.Reservations
                .FirstOrDefault(r => r.UserId == user.UserId &&
                r.Status == Status.Confirmed &&
                r.CarId == car.CarId); // Use Status.Cancelled for comparison

            if (reservation == null)
            {
                return "Reservation not found or already canceled."; // Handle case where the reservation does not exist or is already canceled
            }

            // Update the status of the reservation to "Cancelled"
            reservation.Status = Status.Cancelled; // Assuming Status is an enum with a value 'Cancelled'
            var carStatusChange = _dbContext.Cars.FirstOrDefault(c => c.CarId == reservation.CarId);
            carStatusChange.AvailableStatus = true;
            carStatusChange.AvailableDate = DateOnly.FromDateTime(DateTime.Now.Date);
            _dbContext.SaveChanges();

            return "Reservation canceled successfully."; // Return success message
        }

        
        public List<ReservationDetailsDto> GetReservationHistoryOfUser(string userEmail)
        {
            try
            {
                // Fetch the user based on the provided email
                var user = _dbContext.Users
                    .FirstOrDefault(u => u.Email == userEmail);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                List<ReservationDetailsDto> reservationList = new List<ReservationDetailsDto>();
                
                // Fetch the reservation based on the user ID
                var reservation = _dbContext.Reservations
                    .Where(r => r.UserId == user.UserId).ToList();

                if (reservation.Count() <= 0)
                {
                    return reservationList = new() { };
                }


                foreach (Reservation r in reservation)
                {
                    var car = _dbContext.Cars.FirstOrDefault(c => c.CarId == r.CarId);
                    var location = _dbContext.Locations.FirstOrDefault(l => l.LocationId == car.LocationId);

                    Status s = r.Status;

                    string status;

                    if (s == Status.Confirmed)
                    {

                        status = "Confirmed";
                    }
                    else
                    {
                        status = "Cancelled";
                    }

                    var review = _dbContext.Reviews.FirstOrDefault(r=>r.UserId == user.UserId && r.CarId==car.CarId);

                    if (review == null)
                    {
                        reservationList.Add(new ReservationDetailsDto()
                        {
                            UserEmail = user.Email,  // User's email associated with the reservation
                            CarMake = car.Make,     // Car make
                            CarModel = car.Model,   // Car model
                            CarNumber = car.LicensePlate,
                            Colour = car.Colour,
                            ModelYear= car.year,
                            TotalSeats = car.TotalSeats,
                            ImageUrl = car.ImageUrl,                 
                            City = location.City,
                            Address = location.Address,
                            State = location.State,
                            Country = location.Country,
                            ZipCode = location.ZipCode,
                            ReservationStatus = status,
                            PickUpDate = r.PickUpDate,
                            DropOffDate = r.DropOffDate,
                            TotalAmount = r.Amount,
                            Rating = 0,
                            ReviewText= null,
                            HasReview= false,
                        });
                    }

                    else
                    {
                        reservationList.Add(new ReservationDetailsDto()
                        {
                            UserEmail = user.Email,  // User's email associated with the reservation
                            CarMake = car.Make,     // Car make
                            CarModel = car.Model,   // Car model
                            CarNumber = car.LicensePlate,
                            Colour = car.Colour,
                            ModelYear = car.year,
                            TotalSeats = car.TotalSeats,
                            ImageUrl = car.ImageUrl,
                            City = location.City,
                            Address = location.Address,
                            State = location.State,
                            Country = location.Country,
                            ZipCode = location.ZipCode,
                            ReservationStatus = status,
                            PickUpDate = r.PickUpDate,
                            DropOffDate = r.DropOffDate,
                            TotalAmount = r.Amount,
                            Rating = review.Rating,
                            ReviewText = review.ReviewText,
                            HasReview = review.hasReview,
                        });
                    }
                }
                return reservationList;
            }
            catch (Exception ex)
            {
                // Handle errors and return the exception message
                throw new Exception($"Error fetching reservation details: {ex.Message}");
            }
        }

        public List<AllDetailsReservationDto> GetAllReservation()
        {
            try
            {
                var reservation = _dbContext.Reservations.ToList();

                if (reservation.Count() <= 0)
                {
                    throw new Exception("Reservations not found");
                }

                List<AllDetailsReservationDto> reservationList = new List<AllDetailsReservationDto>();

                foreach (Reservation r in reservation)
                {
                    var user = _dbContext.Users.FirstOrDefault(u => u.UserId == r.UserId);
                    var car = _dbContext.Cars.FirstOrDefault(c => c.CarId == r.CarId);

                    var location = _dbContext.Locations.FirstOrDefault(l => l.LocationId == car.LocationId);

                    Status s = r.Status;

                    string status;

                    if (s == Status.Confirmed)
                    {

                        status = "Confirmed";
                    }
                    else
                    {
                        status = "Cancelled";
                    }


                    reservationList.Add(new AllDetailsReservationDto()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserEmail = user.Email,  // User's email associated with the reservation
                        CarMake = car.Make,     // Car make
                        CarModel = car.Model,   // Car model
                        CarColour = car.Colour,
                        ReservationStatus = status,
                        PickUpDate = r.PickUpDate,
                        DropOffDate = r.DropOffDate,
                        TotalAmount = r.Amount,
                        ImageUrl = car.ImageUrl,
                        CarNumber = car.LicensePlate,
                        TotalSeats = car.TotalSeats,
                        City = location.City,
                        Address = location.Address,
                        State = location.State,
                        Country = location.Country,
                        ZipCode = location.ZipCode
                        
                    });
                }
                return reservationList;
            }
            catch (Exception ex)
            {
                // Handle errors and return the exception message
                throw new Exception($"Error fetching reservation details: {ex.Message}");
            }
        }
    }
}
