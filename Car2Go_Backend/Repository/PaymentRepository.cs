using Car2Go.Data;
using Car2Go.DTOs;
using Car2Go.Models;
using Car2Go.Services;

namespace Car2Go.Repository
{
    public class PaymentRepository : IPaymentService
    {
        private readonly Car2GoDBContext _dbContext;

        public PaymentRepository(Car2GoDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CreatePayment(CreatePaymentDto createPaymentDto,int reservationId)
        {
            Payment newPaymnet;

            if (createPaymentDto == null)
            {
                return false;
            }
            var reservation = _dbContext.Reservations.FirstOrDefault(r => r.ReservationId == reservationId);
            if (reservation != null)
            {

                newPaymnet = new Payment
                {
                    PaymentType = createPaymentDto.PaymentType,
                    PaymentAmount = createPaymentDto.PaymentAmount,
                    PaymentDate = DateTime.Now,
                    PaymentStatus = "Confirmed",
                    ReservationId = reservationId
                };

                _dbContext.Add(newPaymnet);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
