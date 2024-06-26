﻿using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public enum RentOrSale
    {
        [Display(Name ="Location")]
        Rent,
		[Display(Name = "Vente")]
		Sale
	}

    public enum OfferType
    {
		[Display(Name = "Appartement")]
		Appartment,
		[Display(Name = "Maison")]
		House,
		[Display(Name = "Terrain")]
		Land,
	}

	// Your ViewModel (it can be different depending on your actual setup)
	public class OfferDetailsViewModel
	{
		public Offer? Offer { get; set; }
		public List<Appointment>? Appointments { get; set; }
		public List<OfferMedia>? Medias { get; set; }
	}

	public class Offer
    {
		[Key]
		public int OfferId { get; set; }

        [Required]
        public required string Reference { get; set; }

        [Required]
        [EnumDataType(typeof(OfferType))]
        public required string Type { get; set; }

        [Required]
        public int LocationId { get; set; }
        public Location? Location { get; set; }

        [Required]
        [StringLength(300)]
        public required string Description { get; set; }

        public List<int>? OfferMediaId { get; set; } = new List<int>();
        public List<OfferMedia>? OfferMedias { get; set; }

        [Required]
        public required int Price { get; set; }

        public int? Surface { get; set; }

        [Required]
        public required bool Active { get; set; }

        [Required]
        [Range(1,5)]
        public required int ReverseRanking { get; set; }


        [Required]
        [EnumDataType(typeof(RentOrSale))]
        public required string RentOrSale { get; set; }

        public int? AgencyId { get; set; }

        public Agency? Agency { get; set; }

        public int OwnerInfoId { get; set; }
        public OwnerInfo? OwnerInfo { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; } = new DateTime();
    }
}
