using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class Offer
    {
        public int OfferId { get; set; }

        [Required]
        public required string Reference { get; set; }

        [Required]
        public required string Type { get; set; }

        [Required]
        public required Location Location { get; set; }

        [Required]
        public required string Description { get; set; }

        public List<OfferMedia>? Medias { get; set; }

        [Required]
        public required decimal Price { get; set; }

        public decimal? Surface { get; set; }

        [Required]
        public required bool Active { get; set; }

        [Required]
        public required int ReverseRanking { get; set; }

        [Required]
        public required string Status { get; set; }

        [Required]
        public required string RentOrSale { get; set; }

        public int? AgencyId { get; set; }

        public required Agency Agency { get; set; }

        public required OwnerInfo OwnerInfo { get; set; }

        [Required]
        public required bool Archived { get; set; }

        public DateTime? DisabledOn { get; set; }

        public DateTime? ArchivedAt { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; }
    }

  
}
