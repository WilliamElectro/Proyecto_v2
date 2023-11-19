using PGCELL.Shared.Responses;

namespace PGCELL.Backend.Helpers
{
    namespace PGCELL.Backend.Helpers
    {
        public interface IMailHelper
        {
            Response<string> SendMail(string toName, string toEmail, string subject, string body);
        }
    }
}