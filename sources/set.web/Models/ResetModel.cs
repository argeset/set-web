using set.web.Helpers;

namespace set.web.Models
{
    public class ResetModel : BaseModel
    {
        public string Email { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Email)
                   && Email.IsEmail();
        }
    }
}