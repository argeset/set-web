using System.Data.Entity;

namespace set.web.Data.Services
{
    public class BaseService
    {
        public readonly SetDbContext _context;

        public BaseService(SetDbContext context = null)
        {
            if (context == null)
            {
                context = new SetDbContext();
            }

            _context = context;
        }
    }
}