using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }

        [Required]
        public required int UserId { get; set; }

        public User? User { get; set; }

        [Required]
        public required int OfferId { get; set; }

        public Offer? Offer { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; }
    }
}
