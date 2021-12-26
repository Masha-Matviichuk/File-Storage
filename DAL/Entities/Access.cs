namespace DAL.Entities
{
    public class Access : BaseEntity
    {
        public string Modifier { get; set; }
        public virtual File File { get; set; }
    }
}