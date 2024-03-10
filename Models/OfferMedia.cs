using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class OfferMedia
    {
        public int OfferMediaId { get; set; }

        [Required]
        public required string Type { get; set; }

        [Required]
        public required string Path { get; set; }


    }
}
