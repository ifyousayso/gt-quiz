using ITHS.Domain.Interfaces.Entities;

namespace ITHS.Application.ViewModels.Persons;

public class PersonResponse : PersonBase { //rest of the person properties are inherited
    public PersonResponse(IPerson person) {
        base.PhoneNumber = person.PhoneNumber;
        base.EmailAddress = person.EmailAddress;
        base.FirstName = person.FirstName;
        base.LastName = person.LastName;
    }
}
