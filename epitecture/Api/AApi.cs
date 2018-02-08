using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epitecture.Api
{
     public class AApi
    {
        private String Token { set; get; }

        public virtual void Init() { }
        public virtual async Task<IList<Image>> LoadImage() { }
        public virtual async Task<bool> UploadImage() { }
        public virtual async Task<Ilist<Image>> SearchImage(string search = "", size sz = 0, type tp = 0) { }
    }
}
