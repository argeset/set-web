using System;
using System.Threading;

using set.web.Data.Entities;
using set.web.Helpers;
using set.web.Models;

namespace set.web.test.Shared
{
    public class BaseBehaviourTest
    {
        public const string ACTION_LOGIN = "Login";
        public const string ACTION_LOGOUT = "Logout";
        public const string ACTION_NEW = "New";
        public const string ACTION_CONTACT = "Contact";
         
        public static readonly ContactMessageModel VALID_CONTACT_MESSAGE_MODEL = new ContactMessageModel
        {
            Subject = "subject",
            Message = "message",
            Email = "test@test.com"
        };

        public static readonly LoginModel VALID_LOGIN_MODEL = new LoginModel
        {
            Password = "pass",
            Email = "test@test.com"
        };

        public static readonly UserModel VALID_USER_MODEL = new UserModel
        {
            Name = "name",
            Password = "pass",
            Email = "test@test.com",
            Language = Thread.CurrentThread.CurrentUICulture.Name,
            Id = Guid.NewGuid().ToNoDashString()
        };

        public static readonly User VALID_USER_ENTITY = new User
        {
            Id = "1",
            Name = "name",
            Email = "test@test.com",
            RoleName = ConstHelper.User
        }; 
    }
}