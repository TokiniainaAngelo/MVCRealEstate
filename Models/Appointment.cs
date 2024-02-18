using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public required User User { get; set; }

        [Required]
        public int OfferId { get; set; }

        [Required]
        public required Offer Offer { get; set; }

        [Required]
        public DateTime Period { get; set; }

        [Required]
        public bool IsBooked { get; set; }
    }
}
