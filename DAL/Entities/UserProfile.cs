namespace DAL.Entities
{
    public class UserProfile
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public virtual User User { get; set; }
    }
}