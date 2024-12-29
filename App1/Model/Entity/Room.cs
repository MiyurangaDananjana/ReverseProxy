using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App1.Model.Entity
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public required string RoomType { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public required string Status { get; set; }

        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        public int CreateBy { get; set; }

        // Navigation properties
        [ForeignKey("CreateBy")]
        public required User User { get; set; }
        // Navigation properties
        public required ICollection<Booking> Bookings { get; set; }
        

    }
}
