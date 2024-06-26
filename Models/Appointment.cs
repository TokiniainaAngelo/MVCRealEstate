﻿using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [Required]
        public int? UserId { get; set; }

        public User? User { get; set; }

        [Required]
        public int OfferId { get; set; }

        public Offer? Offer { get; set; }

        [Required]
        public DateTime Period { get; set; }

        [Required]
        public bool IsBooked { get; set; }
    }
}
