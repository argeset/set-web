using System.Data.Entity;

using set.web.Data.Entities;

namespace set.web.Data
{
    public class SetDbContext : DbContext
    {
        public SetDbContext(string connectionStringOrName)
            : base(connectionStringOrName)
        {
            Database.SetInitializer(new SetDbInitializer());
        }

        public SetDbContext()
            : this("Name=SetWeb")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<DomainObject> DomainObjects { get; set; }
    }

    public class SetDbInitializer : MigrateDatabaseToLatestVersion<SetDbContext, SetDbMigrationConfiguration>
    {

    }
}
