namespace set.web.Models
{
    public class DomainObjectModel : BaseModel
    {
        public string Name { get; set; }

        internal bool IsValid()
        {
            return !string.IsNullOrEmpty(Name);
        }
    }
}