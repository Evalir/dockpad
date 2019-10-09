using Dockpad.Forms;
using Dockpad.Helpers;
using Dockpad.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dockpad.Services
{
    public interface IPRMAPI
    {
        #region User
        [Post("/users/signup/")]
        Task<HttpResponseMessage> SignUp([Body] SignUpForm data);

        [Post("/users/login/")]
        Task<HttpResponseMessage> Login([Body] LoginForm data);

        [Get("/users/profile/")]
        Task<HttpResponseMessage> GetProfile([Header("Authorization")] string token);
        #endregion

        #region Events
        [Get("/events/")]
        Task<HttpResponseMessage> GetEvents([Header("Authorization")] string token);
        
        [Get("/events/")]
        Task<HttpResponseMessage> GetEventsInterval([Header("Authorization")] string token,
            [AliasAs("from")] string fromDate, [AliasAs("to")] string toDate);

        [Get("/events/{code}")]
        Task<HttpResponseMessage> GetEventDetail([Header("Authorization")] string token, string code);

       // [Post("/events/")]
       // Task<HttpResponseMessage> PostEvent([Header("Authorization")] string token, Event newEvent);
        #endregion

        #region Activities
        [Get("/activities/")]
        Task<HttpResponseMessage> GetActivities([Header("Authorization")] string token);

        [Get("/activities/{code}")]
        Task<HttpResponseMessage> GetActivityDetail([Header("Authorization")] string token, string code);

        [Post("/activities/")]
        Task<HttpResponseMessage> PostActivity([Header("Authorization")] string token, [Body] Activity activity);

        [Put("/activities/{code}")]
        Task<HttpResponseMessage> PutActivity([Header("Authorization")] string token, [Body] Activity activity, string code);

        [Patch("/activities/{code}")]
        Task<HttpResponseMessage> PatchActivity([Header("Authorization")] string token, [Body] Activity activity, string code);
        #endregion

        #region Activity Logs

        [Get("/activities/logs/")]
        Task<HttpResponseMessage> GetActivitiesLogs([Header("Authorization")] string token);

        [Get("/activities/{activityCode}/logs/")]
        Task<HttpResponseMessage> GetActivitiesLogs([Header("Authorization")] string token, string activityCode);

        [Get("/activities/{activityCode}/logs/{logCode}")]
        Task<HttpResponseMessage> GetActivityLogDetail([Header("Authorization")] string token, string activityCode, string logCode);

        [Post("/activities/{activityCode}/logs/")]
        Task<HttpResponseMessage> PostActivityLog([Header("Authorization")] string token, string activityCode, [Body] ActivityLog activityLog);


        [Put("/activities/{activityCode}/logs/{logCode}")]
        Task<HttpResponseMessage> PutActivityLog([Header("Authorization")] string token, string activityCode, [Body] ActivityLog activityLog, string logCode);

        [Patch("/activities/{activityCode}/logs/{logCode}")]
        Task<HttpResponseMessage> PatchActivityLog([Header("Authorization")] string token, string activityCode, [Body] ActivityLog activityLog, string logCode);

        #endregion

        #region Moods
        [Get("/moods/")]
        Task<HttpResponseMessage> GetMoods([Header("Authorization")] string token);

        [Get("/moods/")]
        Task<HttpResponseMessage> GetMoodsInterval([Header("Authorization")] string token, 
            [AliasAs("from")] string fromDate, [AliasAs("to")] string toDate);
        #endregion
    }
}
