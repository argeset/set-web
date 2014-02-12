using set.web.Data.Entities;

namespace set.web.Models
{
    public class DomainObjectModel : BaseModel
    {
        public string Name { get; set; }

        internal bool IsValid()
        {
            return !string.IsNullOrEmpty(Name);
        }

        public static DomainObjectModel MapEntityToModel(DomainObject entity)
        {
            var model = new DomainObjectModel
            {
                Name = entity.Name
            };
            return model;
        }
    }
}