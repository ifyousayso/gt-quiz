using ITHS.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace ITHS.Domain.Entities
{
    public class Role : BaseEntity
    {
        public Role() { /* Default constructor */ }

        public Role(Guid roleId, SchoolRoles roleName) 
        { 
            Id = roleId; 
            RoleName = roleName; 
        }
        
        [MaxLength(15), Required]
        public SchoolRoles RoleName { get; set; }
    }
}
