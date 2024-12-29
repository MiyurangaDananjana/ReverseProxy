using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App1.Model.Entity
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public required string Status { get; set; }

        // Navigation property
        [ForeignKey("BookingId")]
        public required Booking Booking { get; set; }
    }
}