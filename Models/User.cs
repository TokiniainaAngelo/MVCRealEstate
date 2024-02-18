using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class User
    {
        [Required]
        [StringLength(255)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(255)]
        public required string FullName { get; set; }

        [StringLength(255)] 
        public required string Login { get; set; }

        [Required]
        [StringLength(255)]
        public required string Password { get; set; }

        public string? SocketId { get; set; }

        public required string RoleId { get; set; } 
    }
}
