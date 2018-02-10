using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace epitecture.Api
{
    class Api : AApi
    {
        private static readonly HttpClient _client = new HttpClient();

        public enum size { wrong = 0, small = 1, med = 2, big = 3, lrg = 4, huge = 5 };
        public enum type { wrong = 0, jpg = 1, png = 2, gif = 3, anigif = 4, album = 5 };

        public static async Task<HttpResponseMessage> Request(HttpMethod method, String url, String content, Dictionary<String, String> header)
              {
                  var httpRequestMessage = new HttpRequestMessage();
                  httpRequestMessage.Method = method;
                  httpRequestMessage.RequestUri = new Uri(url);
                  foreach (var head in header)
                  {
                      httpRequestMessage.Headers.Add(head.Key, head.Value);
                  }
                  if (method.Method == "POST")
                  {
                      HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
                      httpRequestMessage.Content = httpContent;
                  }
                  return await _client.SendAsync(httpRequestMessage);
              }
    }
}