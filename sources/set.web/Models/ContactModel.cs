using set.web.Helpers;

namespace set.web.Models
{
    public class ContactModel : BaseModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Message) 
                   && Email.IsEmail();
        }
    }
}