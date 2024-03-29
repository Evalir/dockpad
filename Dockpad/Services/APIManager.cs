﻿using Acr.UserDialogs;
using Dockpad.Forms;
using Dockpad.Models;
using Fusillade;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Polly;
using Refit;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dockpad.Services
{
    public class APIManager : IAPIManager
    {
        IAPIService<IPRMAPI> prmApi;

        IUserDialogs _userDialogs = UserDialogs.Instance;
        IConnectivity _connectivity = CrossConnectivity.Current;

        public bool IsConnected { get; set; }

        public APIManager(IAPIService<IPRMAPI> _prmApi)
        {
            prmApi = _prmApi;
            IsConnected = _connectivity.IsConnected;
            _connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.IsConnected;
        }

        protected async Task<TData> RemoteRequestAsync<TData>(Task<TData> task)
            where TData : HttpResponseMessage,
            new()
        {
            TData data = new TData();

            if (!IsConnected)
            {
                string strngResponse = "There's no network connection";
                data.StatusCode = HttpStatusCode.BadRequest;
                data.Content = new StringContent(strngResponse);
                _userDialogs.Toast(strngResponse, TimeSpan.FromSeconds(3));
                return data;
            }

            data = await Policy
             .Handle<WebException>()
             .Or<ApiException>()
             .Or<TaskCanceledException>()
             .WaitAndRetryAsync
             (
                 retryCount: 1,
                 sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
             )
             .ExecuteAsync(async () =>
             {
                 var result = await task;
                 return result;
             });

            return data;
        }

        public async Task<HttpResponseMessage> Login(LoginForm data)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated).Login(data));
        }

        public async Task<HttpResponseMessage> GetProfile(string token)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated).GetProfile(token));
        }

        public async Task<HttpResponseMessage> SignUp(SignUpForm data)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated).SignUp(data));
        }

        public async Task<HttpResponseMessage> GetEvents(string token)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated).GetEvents(token));
        }

        public async Task<HttpResponseMessage> GetActivities(string token)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated).GetActivities(token));
        }

        public async Task<HttpResponseMessage> GetMoods(string token)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated).GetMoods(token));
        }

        public async Task<HttpResponseMessage> GetMoodsInterval(string token, string fromDate, string toDate)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
                .GetMoodsInterval(token, fromDate, toDate));
        }

        public async Task<HttpResponseMessage> GetEventsInterval(string token, string fromDate, string toDate)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
                .GetEventsInterval(token, fromDate, toDate));
        }

        public async Task<HttpResponseMessage> GetEventDetail(string token, string code)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
                .GetEventDetail(token, code));
        }

        public async Task<HttpResponseMessage> PostActivity(string token, Activity activity)
        {
           return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
               .PostActivity(token, activity));
        
        }

        public async Task<HttpResponseMessage> PutActivity(string token, Activity activity, string code)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
                .PutActivity(token, activity, code));
        }

        public async Task<HttpResponseMessage> PatchActivity(string token, Activity activity, string code)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
                .PatchActivity(token, activity, code));
        }

        public async Task<HttpResponseMessage> GetActivitiesLogs(string token)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
                .GetActivitiesLogs(token));
        }

        public async Task<HttpResponseMessage> GetActivitiesLogs(string token, string activityCode)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
                .GetActivitiesLogs(token, activityCode));
        }

        public async Task<HttpResponseMessage> GetActivityLogDetail(string token, string activityCode, string logCode)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
                .GetActivityLogDetail(token, activityCode, logCode));
        }

        public async Task<HttpResponseMessage> PostActivityLog(string token, string activityCode, ActivityLog activityLog)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
                .PostActivityLog(token, activityCode, activityLog));
        }

        public async Task<HttpResponseMessage> PutActivityLog(string token, string activityCode, ActivityLog activityLog, string logCode)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
                .PutActivityLog(token, activityCode, activityLog, logCode));
        }

        public async Task<HttpResponseMessage> PatchActivityLog(string token, string activityCode, ActivityLog activityLog, string logCode)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
                .PatchActivityLog(token, activityCode, activityLog, logCode));
        }

        public async Task<HttpResponseMessage> PatchProfile(string token, Profile profile)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
                .PatchProfile(token, profile));

        }

        public async Task<HttpResponseMessage> PatchUser(string token, string username, User user)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
                .PatchUser(token, username, user));
        }

        public async Task<HttpResponseMessage> PostEvent(string token, Event newEvent)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
                .PostEvent(token, newEvent));
        }

        public async Task<HttpResponseMessage> DeleteEvent(string token, string code) {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
             .DeleteEvent(token, code));
        }

        public async Task<HttpResponseMessage> PatchEvent(string token, string code, Event newEvent)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated)
                .PatchEvent(token, code, newEvent));
        }

        public async Task<HttpResponseMessage> PostMood(string token, Mood mood)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated).PostMood(token, mood));
        }

        public async Task<HttpResponseMessage> GetContacts(string token)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated).GetContacts(token));
        }

        public async Task<HttpResponseMessage> PostContact(string token, Contact contact)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated).PostContact(token, contact));
        }

        public async Task<HttpResponseMessage> PatchContact(string token, string code, Contact contact)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated).PatchContact(token, code, contact));
        }


        public async Task<HttpResponseMessage> DeleteContact(string token, string code)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated).DeleteContact(token, code));
        }

        public async Task<HttpResponseMessage> DeleteActivity(string token, string code)
        {
            return await RemoteRequestAsync(prmApi.GetApi(Priority.UserInitiated).DeleteActivity(token, code));
        }
    }
}
