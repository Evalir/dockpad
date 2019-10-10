using Dockpad.Forms;
using Dockpad.Models;
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

        Task<HttpResponseMessage> DeleteEvent(string token, string code);

        Task<HttpResponseMessage> PatchEvent(string token, string code, Event newEvent);

        Task<HttpResponseMessage> PostMood(string token, Mood mood);

        Task<HttpResponseMessage> GetContacts(string token);

        Task<HttpResponseMessage> PostContact(string token, Contact contact);
        
        Task<HttpResponseMessage> PatchContact(string token, string code, Contact contact);


    }
}
