﻿using System;
using System.Linq;
using System.Threading.Tasks;

using set.web.Data.Entities;
using set.web.Helpers;
using set.web.Models;

namespace set.web.Data.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IMsgService _msgService;

        public UserService(IMsgService msgService)
        {
            _msgService = msgService;
        }

        public Task<bool> Create(UserModel model, string roleName)
        {
            if (model.IsNotValid()) return Task.FromResult(false);

            var img = model.Email.ToGravatar();
            var user = new User
            {
                Id = model.Id,
                Email = model.Email,
                Name = model.Name,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password, 15),
                ImageUrl = img,
                RoleId = ConstHelper.BasicRoles[roleName],
                RoleName = roleName,
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


        public Task<bool> RequestPasswordReset(string email)
        {
            if (!email.IsEmail()) return Task.FromResult(false);

            var user = Context.Users.FirstOrDefault(x => x.IsActive
                                                         && !x.IsDeleted
                                                         && x.Email == email);

            if (user == null) return Task.FromResult(false);

            if (user.PasswordResetRequestedAt != null
               && user.PasswordResetRequestedAt.Value.AddMinutes(-1) > DateTime.Now) return Task.FromResult(false);

            var token = Guid.NewGuid().ToNoDashString();
            user.UpdatedAt = DateTime.Now;
            user.UpdatedBy = user.Id;
            user.PasswordResetToken = token;
            user.PasswordResetRequestedAt = user.UpdatedAt;

            var saved = Context.SaveChanges() > 0;

            if (saved)
            {
                _msgService.SendMail(user.Email, LocalizationHelper.LocalizationString("password_reset_email_subject"), string.Format(LocalizationHelper.LocalizationString("password_reset_email"), user.Email, token));
            }

            return Task.FromResult(saved);
        }

        public Task<bool> IsPasswordResetRequestValid(string email, string token)
        {
            if (!email.IsEmail()) return Task.FromResult(false);

            return Task.FromResult(Context.Users.Any(x => x.IsActive
                                                          && !x.IsDeleted
                                                          && x.Email == email
                                                          && x.PasswordResetToken == token
                                                          && x.PasswordResetRequestedAt >= DateTime.Now.AddHours(-1)));
        }

        public async Task<bool> ChangePassword(string email, string token, string password)
        {
            if (!await IsPasswordResetRequestValid(email, token)) return await Task.FromResult(false);

            var user = Context.Users.FirstOrDefault(x => x.IsActive
                                                         && !x.IsDeleted
                                                         && x.Email == email);

            if (user == null) return await Task.FromResult(false);

            user.UpdatedAt = DateTime.Now;
            user.UpdatedBy = user.Id;
            user.PasswordResetToken = null;
            user.PasswordResetRequestedAt = null;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, 15);
            user.LoginTryCount = 0;

            return await Task.FromResult(Context.SaveChanges() > 0);
        }
    }

    public interface IUserService
    {
        Task<bool> Create(UserModel model, string roleName);

        Task<PagedList<User>> GetUsers(int pageNumber);

        Task<User> Get(string userId);
        Task<User> GetByEmail(string email);

        Task<bool> Authenticate(string email, string password);

        Task<bool> RequestPasswordReset(string email);
        Task<bool> IsPasswordResetRequestValid(string email, string token);
        Task<bool> ChangePassword(string email, string token, string password);
    }
}