using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public enum RoleType
    {
        Admin,
        Moderator,
        User,
    }
    public class Role
    {
        public string Name { get; set; }

        public List<Privilege> Privileges { get; set; }

        [EnumDataType(typeof(RoleType))]
        public string Type { get; set; }
    }
}
