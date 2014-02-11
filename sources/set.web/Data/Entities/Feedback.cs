namespace set.web.Data.Entities
{
    public class Feedback : BaseEntity
    {
        public string Title { get; set; }
        public string Info { get; set; }
        public string Email { get; set; }
        public bool IsAnonymous { get; set; }
    }
}