using Dockpad.Forms;
using Dockpad.Models;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dockpad.Services
{
    public interface IAPIManager
    {
        Task<HttpResponseMessage> Login(LoginForm data);
        Task<HttpResponseMessage> GetProfile(string token);
        Task<HttpResponseMessage> SignUp(SignUpForm data);
        Task<HttpResponseMessage> GetEvents(string token);
        Task<HttpResponseMessage> GetEventsInterval(string token, string fromDate, string toDate);

        Task<HttpResponseMessage> GetEventDetail(string token, string code);
        Task<HttpResponseMessage> GetActivities(string token);

        Task<HttpResponseMessage> PostActivity(string token, Activity activity);

        Task<HttpResponseMessage> PutActivity(string token, Activity activity, string code);

        Task<HttpResponseMessage> PatchActivity(string token, Activity activity, string code);

        Task<HttpResponseMessage> GetMoods(string token);
        Task<HttpResponseMessage> GetMoodsInterval(string token, string fromDate,  string toDate);

        Task<HttpResponseMessage> GetActivitiesLogs(string token);

        Task<HttpResponseMessage> GetActivitiesLogs(string token, string activityCode);

        Task<HttpResponseMessage> GetActivityLogDetail(string token, string activityCode, string logCode);

        Task<HttpResponseMessage> PostActivityLog(string token, string activityCode, ActivityLog activityLog);

        Task<HttpResponseMessage> PutActivityLog(string token, string activityCode, ActivityLog activityLog, string logCode);

        Task<HttpResponseMessage> PatchActivityLog(string token, string activityCode, ActivityLog activityLog, string logCode);

        Task<HttpResponseMessage> PatchProfile(string token, Profile profile);

        Task<HttpResponseMessage> PatchUser(string token, string username, User user);

        Task<HttpResponseMessage> PostEvent(string token, Event newEvent);
    }
}
