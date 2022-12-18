using System.ComponentModel.DataAnnotations;

namespace ITHS.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public DateOnly Startdate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
