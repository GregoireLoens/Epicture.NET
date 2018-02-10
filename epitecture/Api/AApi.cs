using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace epitecture.Api
{
    public class AApi
    {
        private String Token { set; get; }

        public enum size { wrong = 0, small = 1, med = 2, big = 3, lrg = 4, huge = 5 };
        public enum type { wrong = 0, jpg = 1, png = 2, gif = 3, anigif = 4, album = 5 };

        public virtual void Init() { }
        public virtual async Task<Infos> LoadImage() { return null; }
        public virtual async Task UploadImage(String Path) { }
        public virtual async Task<Infos> SearchImage(String search = "", size sz = 0, type tp = 0) { return null; }
        public virtual async Task Fav(String id) { }
    }
}
