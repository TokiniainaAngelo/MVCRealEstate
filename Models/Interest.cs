using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class Interest
    {
        public int InterestRequestId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public int OfferId { get; set; }

        [Required]
        public Offer Offer { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public User Client { get; set; }

        public List<RealEstateMessage> Messages { get; set; }

        [Required]
        public DateTime LastUpdatedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
