using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class User
    {
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(255)] 
        public string Login { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        public string SocketId { get; set; }

        public string RoleId { get; set; } 
    }
}
