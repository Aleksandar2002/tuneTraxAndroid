using AndroidProjekat.Common;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidProjekat
{
    public static class Api
    {   
        private static RestClient _client;
        private static bool authorizationSet = false;
        //static Api()
        //{
        //    _client = new RestClient(BaseUrl);
        //}
        public static string BaseUrl => "http://localhost:5001/api/";

        public static RestClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new RestClient(BaseUrl);
                }

                var user = SecureStorage.Default.GetUser();

                if (user != null && !authorizationSet)
                {
                    authorizationSet = true;
                    _client.AddDefaultHeader("Authorization", "Bearer " + user.Token);
                }

                return _client;
            }
        }

        public static void ClearAuthorizationHeader()
        {
            _client = new RestClient(BaseUrl);
            authorizationSet = false;
        }
    }
}
