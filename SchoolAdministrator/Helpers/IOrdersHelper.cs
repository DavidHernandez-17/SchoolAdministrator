using SchoolAdministrator.Common;
using SchoolAdministrator.Models;

namespace SchoolAdministrator.Helpers
{
    public interface IOrdersHelper
    {
        Task<Response> ProcessOrderAsync(ShowCartViewModel model);
    }

}
