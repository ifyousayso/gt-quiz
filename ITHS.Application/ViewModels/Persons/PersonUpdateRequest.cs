namespace ITHS.Application.ViewModels.Persons
{
    public class PersonUpdateRequest : PersonBase //rest of the person properties are inherited
    {
        public Guid PersonId { get; set; }
    }
}
