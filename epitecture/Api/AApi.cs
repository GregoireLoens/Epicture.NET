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

        public virtual  void Init() { }
        public virtual void LoadImage() { }
        public virtual void UploadImage() { }
        public virtual void SearchImage() { }
    }
}
