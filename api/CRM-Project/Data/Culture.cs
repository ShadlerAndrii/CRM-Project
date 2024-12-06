using CRM_Project.Constants;

namespace CRM_Project.Data
{
    public class Culture
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public CultureName Name { get; set; }
        public double Amount { get; set; }
        public DateTime HarvestDate { get; set; }
    }
}
