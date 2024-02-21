namespace MVCRealEstate.Models
{
    public class Location
    {
        public int LocationId { get; set; }

        public required string Address { get; set; }

        public required string City { get; set; }

        public required string longitude { get; set; }

        public required string latitude { get; set; }
    }
}
