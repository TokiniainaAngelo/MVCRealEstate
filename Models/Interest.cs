using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class Interest
    {
        public int InterestId { get; set; }

        [Required]
        [StringLength(255)]
        public required string Name { get; set; }

        [Required]
        public int OfferId { get; set; }

        public Offer? Offer { get; set; }

        [Required]
        public int UserId { get; set; }

        public User? User { get; set; }

        public List<int>? MessagesId { get; set; }
        public ICollection<Message>? Messages { get; set; }

        [Required]
        public DateTime LastUpdatedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
