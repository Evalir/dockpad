using Dockpad.Forms;
using Dockpad.Models;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dockpad.Services
{
    public interface IAPIManager
    {
        /*
        * This interface contains "Handle{entity}" methods, a handler in this case is a kind of method that implements  POST, PUT and PATCH
        * request methods, this is to avoid the repetition of creating three methods that are basically the same but differ on request method. 
        * In case there were to be differences on they should be handled, the implementation could deal with them as needed. 
        */
        Task<HttpResponseMessage> Login(LoginForm data);
        Task<HttpResponseMessage> GetProfile([Header("Authorization")] string token);
        Task<HttpResponseMessage> SignUp(SignUpForm data);
        Task<HttpResponseMessage> GetEvents([Header("Authorization")] string token);
        Task<HttpResponseMessage> GetEventsInterval([Header("Authorization")] string token,
            [AliasAs("from")] string fromDate, [AliasAs("to")] string toDate);

        Task<HttpResponseMessage> GetEventDetail([Header("Authorization")] string token, string code);
        Task<HttpResponseMessage> GetActivities([Header("Authorization")] string token);

        Task<HttpResponseMessage> PostActivity(string token, Activity activity);

        Task<HttpResponseMessage> PutActivity(string token, Activity activity, string code);

        Task<HttpResponseMessage> PatchActivity(string token, Activity activity, string code);

        Task<HttpResponseMessage> GetMoods([Header("Authorization")] string token);
        Task<HttpResponseMessage> GetMoodsInterval([Header("Authorization")] string token,
            [AliasAs("from")] string fromDate, [AliasAs("to")] string toDate);

        Task<HttpResponseMessage> GetActivitiesLogs([Header("Authorization")] string token);

        Task<HttpResponseMessage> GetActivitiesLogs([Header("Authorization")] string token, string activityCode);

        Task<HttpResponseMessage> GetActivityLogDetail([Header("Authorization")] string token, string activityCode, string logCode);

        Task<HttpResponseMessage> PostActivityLog([Header("Authorization")] string token, string activityCode, ActivityLog activityLog);

        Task<HttpResponseMessage> PutActivityLog([Header("Authorization")] string token, string activityCode, ActivityLog activityLog, string logCode);

        Task<HttpResponseMessage> PatchActivityLog([Header("Authorization")] string token, string activityCode, ActivityLog activityLog, string logCode);

    }
}
