using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class Privilege
    {
        public int PrivilegeId { get; set; }
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Category { get; set; }
    }
}
