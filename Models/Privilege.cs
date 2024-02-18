using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public class Privilege
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
