namespace ITHS.Domain.Models
{
    public class NobelPrizeWinner
    {
        public string awardYear { get; set; }
        public Category category { get; set; }
        public CategoryFullName categoryFullName { get; set; }
        public string dateAwarded { get; set; }
        public int prizeAmount { get; set; }
        public int prizeAmountAdjusted { get; set; }
        public TopMotivation topMotivation { get; set; }
        public List<Laureate> laureates { get; set; }
    }

    public class Category
    {
        public string en { get; set; }
        public string se { get; set; }
        public string no { get; set; }
    }

    public class CategoryFullName
    {
        public string en { get; set; }
        public string se { get; set; }
        public string no { get; set; }
    }

    public class Laureate
    {
        public string id { get; set; }
        public Name name { get; set; }
        public string portion { get; set; }
        public string sortOrder { get; set; }
        public Motivation motivation { get; set; }
        public Links links { get; set; }
    }

    public class Links
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string action { get; set; }
        public string types { get; set; }
    }

    public class Motivation
    {
        public string en { get; set; }
        public string se { get; set; }
        public string no { get; set; }
    }

    public class Name
    {
        public string en { get; set; }
        public string se { get; set; }
        public string no { get; set; }
    }

    public class TopMotivation
    {
        public string en { get; set; }
        public string se { get; set; }
        public string no { get; set; }
    }

}
