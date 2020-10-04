using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MasterWebApp
{
    public static class GlobalVariable
    {

        public static HttpClient webApiClient = new HttpClient();

        //public static string httpBase = "http://localhost:50879/api/";
        public static string httpBase = "https://taxiq8.com/api/";
        static GlobalVariable()
        {
            webApiClient.BaseAddress = new Uri(httpBase);

            webApiClient.DefaultRequestHeaders.Clear();
            webApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applaction/json"));
        }
        public static void addHeader()
        {
            webApiClient.DefaultRequestHeaders.Clear();

            HttpCookie token = HttpContext.Current.Request.Cookies["token"];
            if (token != null && token.Value != null)
            {
                webApiClient.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", token.Value));


            }
            else
            {
                //webApiClient.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", ""));

            }

        }

        public static IEnumerable<object> fullCombo(string address)
        {
            IEnumerable<object> list = null;
            addHeader();
            HttpResponseMessage response = GlobalVariable.webApiClient.GetAsync(address).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                list = response.Content.ReadAsAsync<IEnumerable<object>>().Result;
            }

            return list;
        }

    }
}