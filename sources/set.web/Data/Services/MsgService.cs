namespace set.web.Data.Services
{
    public class MsgService : IMsgService
    {
        public void SendMail(string email, string subject, string htmlBody)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IMsgService
    {
        void SendMail(string email, string subject, string htmlBody);
    }
}