using System;
using System.Threading.Tasks;

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
        //        PublicId = Guid.NewGuid().ToNoDashString(),
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

        //public Task<bool> CreateContactMessage(string name, string email, string title, string info)
        //{
        //    if (string.IsNullOrEmpty(info)) return Task.FromResult(false);

        //    if (string.IsNullOrWhiteSpace(email))
        //    {
        //        email = ConstHelper.Anonymous;
        //    }

        //    var feedback = new Feedback
        //    {
        //        Info = info,
        //        Email = email,
        //        Title = title,
        //        Name = name
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

        //public Task<PagedList<Feedback>> GetFeedbacks(int pageNumber)
        //{
        //    if (pageNumber < 1)
        //    {
        //        pageNumber = 1;
        //    }

        //    var query = _context.Set<Feedback>().Where(x => !x.IsDeleted);

        //    var count = query.Count();
        //    var items = query.OrderByDescending(x => x.Id).Skip(ConstHelper.PageSize * (pageNumber - 1)).Take(ConstHelper.PageSize).ToList();

        //    return Task.FromResult(new PagedList<Feedback>(pageNumber, ConstHelper.PageSize, count, items));
        //}

        //public Task<bool> ChangeStatus(string feedBackPublicId, bool isActive)
        //{
        //    if (string.IsNullOrEmpty(feedBackPublicId)) return Task.FromResult(false);

        //    var feedback = _context.Set<Feedback>().FirstOrDefault(x => x.PublicId == feedBackPublicId);
        //    if (feedback == null) return Task.FromResult(false);

        //    feedback.IsActive = !isActive; 

        //    return Task.FromResult(_context.SaveChanges() > 0);
        //}
        public Task<bool> CreateFeedback(string info, string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateContactMessage(string name, string email, string title, string info)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeStatus(string feedBackPublicId, bool isActive)
        {
            throw new NotImplementedException();
        }
    }

    public interface IFeedbackService
    {
        Task<bool> CreateFeedback(string info, string email);
        Task<bool> CreateContactMessage(string name, string email, string title, string info);
        //Task<PagedList<Feedback>> GetFeedbacks(int pageNumber);
        Task<bool> ChangeStatus(string feedBackPublicId, bool isActive);
    }
}