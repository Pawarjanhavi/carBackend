using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Car2Go.Models
{
    public class Payment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Payment mode is required")]
        public string PaymentType { get; set; }

        [Required]
        public decimal PaymentAmount { get; set; }

        [Required]
        public string PaymentStatus { get; set; }

        public int ReservationId { get; set; }

        //navigation properties
        public Reservation reservation { get; set; }
    }
}
