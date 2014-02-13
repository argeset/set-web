using System;
using System.Linq;
using System.Threading.Tasks;

using set.web.Data.Entities;
using set.web.Helpers;
using set.web.Models;

namespace set.web.Data.Services
{
    public class UserService : BaseService, IUserService
    {
        public Task<bool> Create(UserModel model, string roleName)
        {
            if (model.IsNotValid()) return Task.FromResult(false);

            var img = model.Email.ToGravatar();
            var user = new User
            {
                Email = model.Email,
                Name = model.Name,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                ImageUrl = img,
                RoleId = ConstHelper.BasicRoles[roleName],
                RoleName = roleName,
                IsActive = true,
                Language = model.Language
            };
            Context.Set<User>().Add(user);

            return Task.FromResult(Context.SaveChanges() > 0);
        }

        public Task<PagedList<User>> GetUsers(int pageNumber)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var query = Context.Set<User>().Where(x => !x.IsDeleted);

            var count = query.Count();
            var items = query.OrderByDescending(x => x.Id).Skip(ConstHelper.PageSize * (pageNumber - 1)).Take(ConstHelper.PageSize).ToList();

            return Task.FromResult(new PagedList<User>(pageNumber, ConstHelper.PageSize, count, items));
        }

        public Task<PagedList<User>> GetUsersByRoleId(int roleId, int pageNumber)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var query = Context.Set<User>().Where(x => !x.IsDeleted && x.RoleId == roleId);

            var count = query.Count();
            var items = query.OrderByDescending(x => x.Id).Skip(ConstHelper.PageSize * (pageNumber - 1)).Take(ConstHelper.PageSize).ToList();

            return Task.FromResult(new PagedList<User>(pageNumber, ConstHelper.PageSize, count, items));
        }

        public Task<User> Get(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return null;

            var user = Context.Set<User>().FirstOrDefault(x => x.Id == userId);
            return Task.FromResult(user);
        }
        public Task<User> GetByEmail(string email)
        {
            if (!email.IsEmail()) return null;

            var user = Context.Set<User>().FirstOrDefault(x => x.Email == email);
            return Task.FromResult(user);
        }

        public Task<bool> Authenticate(string email, string password)
        {
            if (!email.IsEmail() || string.IsNullOrEmpty(password)) return Task.FromResult(false);

            var user = Context.Set<User>().FirstOrDefault(x => x.Email == email);
            if (user == null) return Task.FromResult(false);

            var result = false;

            if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)
                && user.LoginTryCount < 5)
            {
                user.LastLoginAt = DateTime.Now;
                user.LoginTryCount = 0;
                result = true;
            }
            else
            {
                user.LoginTryCount += 1;
            }

            Context.SaveChanges();

            return Task.FromResult(result);
        }
    }

    public interface IUserService
    {
        Task<bool> Create(UserModel model, string roleName);

        Task<PagedList<User>> GetUsers(int pageNumber);
        Task<PagedList<User>> GetUsersByRoleId(int roleId, int pageNumber);

        Task<User> Get(string userId);
        Task<User> GetByEmail(string email);

        Task<bool> Authenticate(string email, string password);
    }
}