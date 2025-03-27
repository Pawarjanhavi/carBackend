using System.ComponentModel.DataAnnotations;

namespace Car2Go.DTOs
{
    public class CreatePaymentDto
    {
        [Required]
        public string PaymentType { get; set; }

        [Required]
        public decimal PaymentAmount { get; set; }

        //[Required]
        //public string PaymentStatus { get; set; }
    }
}
