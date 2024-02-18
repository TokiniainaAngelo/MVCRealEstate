using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class Offer
    {
        public int OfferId { get; set; }

        [Required]
        public string Reference { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public Location Location { get; set; }

        [Required]
        public string Description { get; set; }

        public List<OfferMedia> Medias { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal? Surface { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public int ReverseRanking { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string RentOrSale { get; set; }

        public int? AgencyId { get; set; }

        public Agency Agency { get; set; }

        public OwnerInfo OwnerInfo { get; set; }

        [Required]
        public bool Archived { get; set; }

        public DateTime? DisabledOn { get; set; }

        public DateTime? ArchivedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }

  
}
