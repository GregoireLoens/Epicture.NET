﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;

namespace epitecture.Api.Imgur
{
    class Imgur : Api
    {
        public override async Task Fav(String id)
        {
            var method = new HttpMethod("POST");
            var url = "https://api.imgur.com/3/image/";
            Dictionary<string, string> header = new Dictionary<string, string>
                 {
                     {"Authorization", "Bearer a7e66bd16f106e8ac0a514be2d4c869ed497c299"}
                 };
            url = url + id + "/favorite";
            var task = await Request(method, url, header);
        }

        public override void Init()
        {}

        public override async Task<IList<Img>> LoadImage()
        {
            var method = new HttpMethod("GET");
            var url = "https://api.imgur.com/3/gallery/hot/";
            Dictionary<string, string> header = new Dictionary<string, string>
                 {
                     {"Authorization", "Bearer a7e66bd16f106e8ac0a514be2d4c869ed497c299"}
                 };
            var task = await Request(method, url, header);
            if (task.IsSuccessStatusCode)
            {
                var result = await task.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<Infos>(result);
                var img = GetImageData(json);
                return (img);
            }
            return (null);
        }

        public override async Task<IList<Img>> SearchImage(string search = "", size sz = 0, type tp = 0)
        {
            char transition = '?';
            var method = new HttpMethod("GET");
            var url = "https://api.imgur.com/3/gallery/search/";
            Dictionary<string, string> header = new Dictionary<string, string>
                 {
                     {"Authorization", "Bearer a7e66bd16f106e8ac0a514be2d4c869ed497c299"}
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
                transition = '&';
            }
            if (tp != 0)
            {
                switch (tp)
                {
                    case (type.jpg):
                        url = url + transition + "q_type=jpg";
                        break;
                    case (type.png):
                        url = url + transition + "q_type=png";
                        break;
                    case (type.gif):
                        url = url + transition + "q_type=gif";
                        break;
                    case (type.anigif):
                        url = url + transition + "q_type=anigif";
                        break;
                    case (type.album):
                        url = url + transition + "q_type=album";
                        break;
                    default:
                        break;

                }
                transition = '&';
            }
            if (search.Length != 0)
            {
                var tmp = string.Concat(transition + "q_any=", search);
                url = string.Concat(url, tmp);
            }
            var task = await Request(method, url, header);
            if (task.IsSuccessStatusCode)
            {
                var result = await task.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<Infos>(result);
                var img = GetImageData(json);
                return (img);
            }
            return (null);
        }

        public async Task<string> EncodeAsync(StorageFile file)
        {
            byte[] byteArray;
            using (var inputStream = await file.OpenSequentialReadAsync())
            {
                var readStream = inputStream.AsStreamForRead();
                byteArray = new byte[readStream.Length];
                await readStream.ReadAsync(byteArray, 0, byteArray.Length);
            }
            string base64 = Convert.ToBase64String(byteArray);
            return base64;
        }

        public async Task UploadImage(Img img)
        {
            var method = new HttpMethod("POST");
            var url = "https://api.imgur.com/3/image";
            var base64 = await EncodeAsync(img.file);
            var content = new Dictionary<String, String>
                    {
                        {"image", base64},
                        {"type", "base64"},
                        {"title", img.title}
                    };

            Dictionary<string, string> header = new Dictionary<string, string>
                         {
                             {"Authorization", "Bearer a7e66bd16f106e8ac0a514be2d4c869ed497c299"}
                         };
            var task = await Request(method, url, header, content);
        }
    }
}
