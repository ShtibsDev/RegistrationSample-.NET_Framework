using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using RegistrationSample.OldDesktopUI.Library.Models;

namespace RegistrationSample.OldDesktopUI.Library.API
{
    public class ApiHelper : IApiHelper
    {
        public ApiHelper()
        {
            InitializeClient();
        }

        public HttpClient ApiClient { get; private set; }

        private void InitializeClient()
        {
            var apiUri = ConfigurationManager.AppSettings["API"];
            ApiClient = new HttpClient { BaseAddress = new Uri(apiUri) };
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}