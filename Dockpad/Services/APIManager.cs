using Dockpad.Forms;
using Fusillade;
using Polly;
using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dockpad.Services
{
    public class APIManager : IAPIManager
    {
        IAPIService<IPRMAPI> prmApi;

        public APIManager(IAPIService<IPRMAPI> _prmApi)
        {
            prmApi = _prmApi;
        }

        protected async Task<TData> RemoteRequestAsync<TData>(Task<TData> task)
            where TData : HttpResponseMessage,
            new()
        {
            TData data = new TData();

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

        public async Task<HttpResponseMessage> Login([Body] LoginForm data)
        {
            return await RemoteRequestAsync<HttpResponseMessage>(prmApi.GetApi(Priority.UserInitiated).Login(data));
        }

        public async Task<HttpResponseMessage> GetProfile(string token)
        {
            return await RemoteRequestAsync<HttpResponseMessage>(prmApi.GetApi(Priority.UserInitiated).GetProfile(token));
        }

        public async Task<HttpResponseMessage> SignUp([Body] SignUpForm data)
        {
            return await RemoteRequestAsync<HttpResponseMessage>(prmApi.GetApi(Priority.UserInitiated).SignUp(data));
        }

        public async Task<HttpResponseMessage> GetAllEvents(string token)
        {
            return await RemoteRequestAsync<HttpResponseMessage>(prmApi.GetApi(Priority.UserInitiated).GetAllEvents(token));
        }
    }
}
