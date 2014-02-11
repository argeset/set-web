using System;

namespace set.web.Data.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = UpdatedAt = DateTime.Now;
            IsDeleted = false;
            IsActive = true;
        }

        public string Id { get; set; }

        public bool IsActive { get; set; }
        public string Name { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}