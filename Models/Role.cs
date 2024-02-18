using System.ComponentModel.DataAnnotations;

namespace MVCRealEstate.Models
{
    public enum RoleType
    {
        Admin,
        User,
    }
    public class Role
    {
        public int RoleId { get; set; }
        public required string Name { get; set; }

        public List<int> PrivilegeId { get; set; }
        public List<Privilege>? Privileges { get; set; }

        [EnumDataType(typeof(RoleType))]
        public required string Type { get; set; }
    }
}
