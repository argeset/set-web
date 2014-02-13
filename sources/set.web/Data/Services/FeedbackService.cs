using System.Linq;
using System.Threading.Tasks;

using set.web.Data.Entities;
using set.web.Helpers;

namespace set.web.Data.Services
{
    public class FeedbackService : BaseService, IFeedbackService
    {
        public Task<bool> CreateFeedback(string message, string email)
        {
            if (string.IsNullOrWhiteSpace(message)) return Task.FromResult(false);

            if (string.IsNullOrWhiteSpace(email))
            {
                email = ConstHelper.Anonymous;
            }

            var feedback = new Feedback
            {
                Message = message,
                Email = email
            };

            var user = Context.Set<User>().FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                feedback.CreatedBy = user.Id;
            }

            Context.Set<Feedback>().Add(feedback);

            return Task.FromResult(Context.SaveChanges() > 0);
        }

        public Task<bool> CreateContactMessage(string subject, string email, string message)
        {
            var contact = new ContactMessage
            {
                Subject = subject,
                Email = email,
                Message = message
            };

            var user = Context.Set<User>().FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                contact.CreatedBy = user.Id;
            }

            Context.Set<ContactMessage>().Add(contact);

            return Task.FromResult(Context.SaveChanges() > 0);
        }
    }

    public interface IFeedbackService
    {
        Task<bool> CreateFeedback(string message, string email);
        Task<bool> CreateContactMessage(string subject, string email, string message);
    }
}