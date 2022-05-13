using SchoolAdministrator.Common;

namespace SchoolAdministrator.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string toName, string toEmail, string subject, string body);
    }
}
