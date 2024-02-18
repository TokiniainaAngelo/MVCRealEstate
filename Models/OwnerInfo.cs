using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class OwnerInfo
    {
        public int OwnerInfoId { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        [Required]
        public List<string> Phones { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }
    }
}
