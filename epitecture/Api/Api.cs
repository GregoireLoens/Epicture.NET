using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace epitecture.Api
{
    class Api: AApi
    {
        private static readonly HttpClient _client = new HttpClient();

        static async Task<HttpResponseMessage> Request(HttpMethod method, String url, String content, Dictionary<String, String> header) 
        {
            HttpRequestMessage request = new HttpRequestMessage();
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = method;
            httpRequestMessage.RequestUri = new Uri(url);
            foreach (var head in header)
            {
                httpRequestMessage.Headers.Add(head.Key, head.Value);
            }
            switch (method.Method)
            {
                case "POST":
                    HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
                    httpRequestMessage.Content = httpContent;
                    break;

            }

            return await _client.SendAsync(request);
        }
        public override void Init()
        {

        }
        public override void LoadImage()
        {

        }
        public override void UploadImage()
        {

        }
        public override void SearchImage()
        {

        }
    }
}
