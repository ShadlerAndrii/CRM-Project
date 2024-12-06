using CRM_Project.Constants;

namespace CRM_Project.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Phone {  get; set; }
        public DateTime Birthday { get; set; }
        public UserSex Sex { get; set; }
        public UserRole Role { get; set; }
    }
}