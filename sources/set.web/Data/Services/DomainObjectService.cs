using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using set.web.Data.Entities;
using set.web.Helpers;

namespace set.web.Data.Services
{
    public class DomainObjectService : BaseService, IDomainObjectService
    {
        public Task<bool> Create(string name, string email)
        {
            var model = new DomainObject() { Name = name };

            var user = _context.Set<User>().FirstOrDefault(x => x.Email == email);
            if (user != null)
                model.CreatedBy = user.Id;

            _context.Set<DomainObject>().Add(model);
            return Task.FromResult(_context.SaveChanges() > 0);
        }

        public Task<PagedList<DomainObject>> GetDomainObjects(int pageNumber)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            var query = _context.Set<DomainObject>();

            var count = query.Count();
            var items = query.OrderByDescending(x => x.Id).Skip(ConstHelper.PageSize * (pageNumber - 1)).Take(ConstHelper.PageSize).ToList();

            return Task.FromResult(new PagedList<DomainObject>(pageNumber, ConstHelper.PageSize, count, items));
        }

        public Task<List<DomainObject>> GetAll()
        {
            return Task.FromResult(new List<DomainObject>(_context.Set<DomainObject>()));
        }

        public Task<List<DomainObject>> Search(string key)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IDomainObjectService
    {
        Task<bool> Create(string name, string email);
        Task<PagedList<DomainObject>> GetDomainObjects(int pageNumber);
        Task<List<DomainObject>> GetAll();
        Task<List<DomainObject>> Search(string key);
    }
}