using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App1.Model.Entity
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string Status { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public required User User { get; set; }

        [ForeignKey("RoomId")]
        public required Room Room { get; set; }

        public required ICollection<Payment> Payments { get; set; }
    }
}
