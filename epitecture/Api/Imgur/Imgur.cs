using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace epitecture.Api.Imgur
{
    class Imgur : Api
    {
        public override Task<IList<bool>> addToFav()
        {
            throw new System.NotImplementedException();
        }

        public override Task<IList<bool>> delFav()
        {
            throw new System.NotImplementedException();
        }

        public override void Init()
        {}

      public override async Task<IList<Img>> LoadImage()
      {
                 var method = new HttpMethod("get");
                 var url = "https://api.imgur.com/3/gallery/hot";
                 var content = "";
                 Dictionary<string, string> header = new Dictionary<string, string>
                 {
                     {"Authorization", "Client-ID 55c8986212a8f48"}
                 };
                 var task = await Request(method, url, content, header);
            if (task.IsSuccessStatusCode)
            {
                var result = task.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<Infos>(result.Result);
                foreach (var tmp in json.data)
                {
                    if (tmp.images != null)
                    {
                        foreach (var tmp2 in tmp.images)
                        {
                            if (tmp2 != null)
                            {
                                //get içi les infos dont on a besoin chaque tmp2 est un obj image
                            }
                        }
                    }
                }
            }
            return (null);
      }

        public override async Task<Infos> SearchImage(string search = "", size sz = 0, type tp = 0)
        {
            var method = new HttpMethod("get");
            var url = "https://api.imgur.com/3/gallery/search/";
            var content = "";
            Dictionary<string, string> header = new Dictionary<string, string>
                 {
                     {"Authorization", "Client-ID 55c8986212a8f48"}
                 };
            if (sz != 0)
            {
                switch (sz)
                {
                    case (size.small):
                        url = url + "?q_size_px=small";
                        break;
                    case (size.med):
                        url = url + "?q_size_px=med";
                        break;
                    case (size.big):
                        url = url + "?q_size_px=big";
                        break;
                    case (size.lrg):
                        url = url + "?q_size_px=lrg";
                        break;
                    case (size.huge):
                        url = url + "?q_size_px=huge";
                        break;
                    default:
                        break;

                }
            }
            else if (tp != 0)
            {
                switch (tp)
                {
                    case (type.jpg):
                        url = url + "?q_type=jpg";
                        break;
                    case (type.png):
                        url = url + "?q_type=png";
                        break;
                    case (type.gif):
                        url = url + "?q_type=gif";
                        break;
                    case (type.anigif):
                        url = url + "?q_type=anigif";
                        break;
                    case (type.album):
                        url = url + "?q_type=album";
                        break;
                    default:
                        break;

                }
            }
            else
            {
                var tmp = string.Concat("?q_any=", search);
                url = string.Concat(url, tmp);
            }
            var task = await Request(method, url, content, header);
            if (task.IsSuccessStatusCode)
            {
                var result = task.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<Infos>(result.Result);
                foreach (var tmp in json.data)
                {
                    if (tmp.images != null)
                    {
                        foreach (var tmp2 in tmp.images)
                        {
                            if (tmp2 != null)
                            {
                                //get içi les infos dont on a besoin chaque tmp2 est un obj image
                            }
                        }
                    }
                }
            }
            return (null);
        }

        public string Encode(String Path)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(Path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;
        }

        public override async Task<bool> UploadImage(String Path)
        {
            var method = new HttpMethod("post");
            var url = "https://api.imgur.com/3/image";
            var base64 = Encode(Path);
            var content = "image=" + base64 + ",type=base64";

            Dictionary<string, string> header = new Dictionary<string, string>
                 {
                     {"Authorization", "Bearer a7e66bd16f106e8ac0a514be2d4c869ed497c299"}
                 };
            var task = await Request(method, url, content, header);
            return true;
        }
    }
}
