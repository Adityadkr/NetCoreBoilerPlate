using CommonEntities.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CommonEntities.Helpers
{
    public static class HttpHelper
    {


        public static async Task<HttpResponseMessage> Get(string url, string jwtToken = null, Dictionary<string, string> headers = null)
        {
            try
            {

                using (var client = new HttpClient())
                {

                    HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

                    if (jwtToken != null)
                    {
                        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    }
                    if (headers != null)
                    {
                        if (headers.Count > 0)
                        {
                            foreach (var item in headers)
                            {
                                client.DefaultRequestHeaders.Add(item.Key, item.Value);
                            }
                        }
                    }
                    //client.DefaultRequestHeaders.Accept.Clear();

                    // httpRequest.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    var response = await client.SendAsync(httpRequest);

                    return response;
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public static async Task<HttpResponseMessage> Post<T>(string url, T content, string contentType, string jwtToken = null, Dictionary<string, string> headers = null)
        {
            try
            {

                using (var client = new HttpClient())
                {

                    HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

                    if (jwtToken != null)
                    {
                        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    }
                    if (headers != null)
                    {
                        if (headers.Count > 0)
                        {
                            foreach (var item in headers)
                            {
                                client.DefaultRequestHeaders.Add(item.Key, item.Value);
                            }
                        }
                    }
                    //client.DefaultRequestHeaders.Accept.Clear();

                    if (contentType == ContentType.JSON)
                    {
                        httpRequest.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
                    }
                    if (contentType == ContentType.XML)
                    {
                        //httpRequest.Content = new xml(content, Encoding.UTF8, "application/json"); ;
                    }
                    var response = await client.SendAsync(httpRequest);

                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }

}
