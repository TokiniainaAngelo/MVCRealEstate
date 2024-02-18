using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public enum MessageSenderType
    {
        Admin,
        Client,
    }
    public class InterestMessage
    {
        public int RealEstateMessageId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [EnumDataType(typeof(MessageSenderType))]
        public string SenderType { get; set; }

        [Required]
        [StringLength(255)]
        public string SenderFullName { get; set; }

        [Required]
        public int InterestRequestId { get; set; }

        [Required]
        public Interest Interest { get; set; }
    }
}
