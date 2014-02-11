﻿using System;
using System.Linq;
using System.Threading.Tasks;
using set.web.Data.Entities;
using set.web.Helpers;
using set.web.Models;

namespace set.web.Data.Services
{
    public class FeedbackService : BaseService, IFeedbackService
    {
        //public Task<bool> CreateFeedback(string info, string email)
        //{
        //    if (string.IsNullOrEmpty(info)) return Task.FromResult(false);

        //    if (string.IsNullOrWhiteSpace(email))
        //    {
        //        email = ConstHelper.Anonymous;
        //    }

        //    var feedback = new Feedback
        //    {
        //        Id =Guid.NewGuid().ToNoDashString(),
        //        Info = info,
        //        Email = email
        //    };

        //    var user = _context.Set<User>().FirstOrDefault(x => x.Email == email);
        //    if (user != null)
        //    {
        //        feedback.IsAnonymous = true;
        //        feedback.CreatedBy = user.Id;
        //    }

        //    _context.Set<Feedback>().Add(feedback);

        //    return Task.FromResult(_context.SaveChanges() > 0);
        //}

        public Task<bool> CreateContactMessage(string subject, string email, string message)
        {
            var contact = new Contact
            {
                Subject = subject,
                Email = email,
                Message = message
            };

            var user = _context.Set<User>().FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                contact.IsAnonymous = false;
                contact.CreatedBy = user.Id;
            }

            _context.Set<Contact>().Add(contact);

            return Task.FromResult(_context.SaveChanges() > 0);
        }

        public Task<bool> CreateFeedback(string info, string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeStatus(string feedBackId, bool isActive)
        {
            throw new NotImplementedException();
        }
    }

    public interface IFeedbackService
    {
        Task<bool> CreateFeedback(string info, string email);
        Task<bool> CreateContactMessage(string subject, string email, string message);
        Task<bool> ChangeStatus(string feedBackId, bool isActive);
    }
}