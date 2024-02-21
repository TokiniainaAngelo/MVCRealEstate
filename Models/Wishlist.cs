using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }

        [Required]
        public required int UserId { get; set; }

        [Required]
        public User? User { get; set; }

        [Required]
        public required int OfferId { get; set; }

        [Required]
        public Offer? Offer { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; }
    }
}
