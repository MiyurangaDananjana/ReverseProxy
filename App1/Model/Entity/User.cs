using System.ComponentModel.DataAnnotations;

namespace App1.Model.Entity
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public required string Password { get; set; }

        [Required]
        public int Role { get; set; }


    }
}
