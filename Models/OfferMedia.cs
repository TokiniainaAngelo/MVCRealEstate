using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class OfferMedia
    {
        public int OfferMediaId { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Path { get; set; }
    }
}
