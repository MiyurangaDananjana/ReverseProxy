using System.ComponentModel.DataAnnotations;

namespace App1.Model.Entity
{
    public class UserRoleStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public DateTime CreateDate { get; set; } 
    }
}
