using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class Wishlist
    {
        public int RealEstateWishlistId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public int OfferId { get; set; }

        [Required]
        public Offer Offer { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
