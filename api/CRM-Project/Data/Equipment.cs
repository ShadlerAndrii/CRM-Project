using CRM_Project.Constants;

namespace CRM_Project.Data
{
    public class Equipment
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Model { get; set; }
        public EquipmentType Type { get; set; }
    }
}
