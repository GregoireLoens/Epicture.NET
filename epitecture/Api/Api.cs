using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace epitecture.Api
{
    public class Api : AApi
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<HttpResponseMessage> Request(HttpMethod method, String url, Dictionary<String, String> header, Dictionary<String, String> content = null)
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
                    if (content != null)
                        httpRequestMessage.Content = new FormUrlEncodedContent(content.ToArray());
                  }
                  return await _client.SendAsync(httpRequestMessage);
              }

        public static IList<Img> GetImageData(Infos data)
        {
            IList<Img> img = new List<Img>();

            foreach (var dt in data.data)
            {
                if (dt.images == null)
                    continue;
                foreach (var tmp in dt.images)
                {
                    if (tmp != null)
                    {
                        tmp.data = new BitmapImage(new Uri(tmp.link));
                        img.Add(tmp);
                    }
                }
            }
            return (img);
        }
    }
}