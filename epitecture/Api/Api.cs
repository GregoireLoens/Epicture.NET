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
                    httpRequestMessage.Content = new FormUrlEncodedContent(content.ToArray());
                  }
                  return await _client.SendAsync(httpRequestMessage);
              }

        public static async Task<IList<Img>> GetImageData(Infos data)
        {
            IList<Img> img = new List<Img>();

            foreach (var dt in data.data)
            {
                foreach (var tmp in dt.images)
                {
                    if (tmp != null)
                    {
                        System.Net.WebRequest request = System.Net.WebRequest.Create(tmp.link);
                        System.Net.WebResponse response = await request.GetResponseAsync();
                        System.IO.Stream responseStream = response.GetResponseStream();
                        BitmapImage bitmap2 = new BitmapImage();
                        var memSream = new MemoryStream();
                        await responseStream.CopyToAsync(memSream);
                        bitmap2.SetSource(memSream.AsRandomAccessStream());
                        tmp.data = bitmap2;
                        img.Add(tmp);
                    }
                }
            }
            return (img);
        }
    }
}