using System;
using System.Collections.Generic;
using System.Net.Http;

namespace epitecture.Api.Imgur
{
    class Imgur : Api
    {
        public override void Init()
        {
            base.Init();
        }

      public void LoadImage()
             {
                 var method = new HttpMethod("get");
                 var url = "https://api.imgur.com/3/gallery/hot";
                 var content = "";
                 Dictionary<string, string> header = new Dictionary<string, string>
                 {
                     {"Authorization", "Client-ID 55c8986212a8f48"}
                 };
                 var task = Request(method, url, content, header);
                 var response = task.Result;
                 var result = response.Content.ReadAsStringAsync();
                 var json = JsonConvert.DeserializeObject<Infos>(result.Result);
                 foreach (var tmp in json.data)
                 {
                     if (tmp.images != null)
                     {
                         foreach (var tmp2 in tmp.images)
                         {
                             if (tmp2 != null)
                             {
                                 Console.WriteLine(tmp2.link);
                             }
                         }
                     }
                 }
             }

        public override void SearchImage()
        {
            base.SearchImage();
        }

        public override void UploadImage()
        {
            base.UploadImage();
        }
    }
}
