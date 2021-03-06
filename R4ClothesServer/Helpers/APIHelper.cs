using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace R4ClothesServer.Helpers
{
    public class APIHelper : IAPIHelper
    {
        public IConfiguration _config;
        public ILocalStorageService _localStorage;
        
        public APIHelper(IConfiguration config, ILocalStorageService localStorageService)
        {
            _localStorage = localStorageService;
            _config = config;
        }

        public async Task<string> GetRequestAsync(string url, string token)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            //HttpClient client = new HttpClient(clientHandler);
            var urlapi = _config.GetSection("API")["APIUrl"].ToString();
            var token2 = await _localStorage.GetItemAsync<string>("token");
            using (var client = new HttpClient(clientHandler))
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token2);
                    HttpResponseMessage res = await client.GetAsync(urlapi + url);
                    if (res.IsSuccessStatusCode)
                    {
                        return res.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        return "-1";
                    }
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        public async Task<string> PostRequestAsync(string url, object postData, string token)
        {
            var urlapi = _config.GetSection("API")["APIUrl"].ToString();
            string token2 = null;
            if (token != "null")
            {
                token2 = await _localStorage.GetItemAsync<string>("token");
                
            }
            else
            {
                token = null;
            }
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            //HttpClient client = new HttpClient(clientHandler);
            using ( var client = new HttpClient(clientHandler))
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token2);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await client.PostAsync(urlapi + url, content);
                    if (res.IsSuccessStatusCode)
                    {
                        return res.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        return "-1";
                    }
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        public async Task<string> PuttRequestAsync(string url, object postData, string token)
        {
            var urlapi = _config.GetSection("API")["APIUrl"].ToString();
            string token2 = null;
            if (token != "null")
            {
                token2 = await _localStorage.GetItemAsync<string>("token");

            }
            else
            {
                token = null;
            }
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            //HttpClient client = new HttpClient(clientHandler);
            using (var client = new HttpClient(clientHandler))
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token2);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await client.PutAsync(urlapi + url, content);
                    if (res.IsSuccessStatusCode)
                    {
                        return res.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        return "-1";
                    }
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
    }
}
