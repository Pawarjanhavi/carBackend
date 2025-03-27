using Car2Go.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Car2Go.Services
{
    public interface IPaymentService
    {
        public bool CreatePayment(CreatePaymentDto createPaymentDto,int reservationId); 

    }
}
