using set.web.Data.Entities;

namespace set.web.Models
{
    public class FeedbackModel : BaseModel
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        
        public static FeedbackModel Map(Feedback feedback)
        {
            var model = new FeedbackModel
            {
                Email = feedback.Email,
                Id = feedback.Id,
                Message = feedback.Message
            };
            return model;
        }
    }
}