using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class Agency
    {
        public int AgencyId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public List<string> Phones { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Logo { get; set; }

        [Required]
        public bool CanViewWishlist { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
