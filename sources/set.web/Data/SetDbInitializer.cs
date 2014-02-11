using System.Data.Entity;

namespace set.web.Data
{
    public class SetDbInitializer : MigrateDatabaseToLatestVersion<SetDbContext, SetDbMigrationConfiguration>
    {

    }
}