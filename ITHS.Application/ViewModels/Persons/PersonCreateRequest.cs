using ITHS.Domain.Constants;

namespace ITHS.Application.ViewModels.Persons
{
    public class PersonCreateRequest : PersonBase //rest of the person properties are inherited
    {
        public SchoolRoles RoleName { get; set; }
    }
}
